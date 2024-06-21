CREATE TABLE IF NOT EXISTS public.categories
(
    category_id SERIAL PRIMARY KEY,
    category_name VARCHAR(255),
    category_image TEXT
);

CREATE TABLE IF NOT EXISTS public.subcategories
(
    subcategory_id SERIAL PRIMARY KEY,
    category_id INT,
    subcategory_name VARCHAR(255),
    subcategory_image TEXT,
    FOREIGN KEY (category_id) REFERENCES public.categories(category_id)
);

CREATE TABLE IF NOT EXISTS public.products
(
    product_id SERIAL PRIMARY KEY,
    calories INT,
    carbohydrates NUMERIC(6, 2),
    composition TEXT,
    description TEXT,
    fats NUMERIC(6, 2),
    image TEXT,
    manufacturer VARCHAR(255),
    product_name VARCHAR(255),
    proteins NUMERIC(6, 2),
    shelf_life INT,
    storage_conditions TEXT,
    subcategory_id INT,
    FOREIGN KEY (subcategory_id) REFERENCES public.subcategories(subcategory_id)
);

CREATE TABLE IF NOT EXISTS public.chain_of_stores
(
    chain_id SERIAL PRIMARY KEY,
    chain_name VARCHAR(255),
    description TEXT,
    chain_image TEXT
);

CREATE TABLE IF NOT EXISTS public.stores
(
    store_id SERIAL PRIMARY KEY,
    address TEXT,
    chain_id INT,
    city VARCHAR(100),
    store_name VARCHAR(255),
    FOREIGN KEY (chain_id) REFERENCES public.chain_of_stores(chain_id)
);

CREATE TABLE IF NOT EXISTS public.warehouse
(
    warehouse_id SERIAL PRIMARY KEY,
    product_id INT,
    product_price NUMERIC(10, 2),
    quantity INT,
    store_id INT,
    FOREIGN KEY (product_id) REFERENCES public.products(product_id),
    FOREIGN KEY (store_id) REFERENCES public.stores(store_id)
);

CREATE TABLE IF NOT EXISTS public.delivery_options
(
    delivery_id SERIAL PRIMARY KEY,
    delivery_type VARCHAR(255),
    delivery_price NUMERIC(10, 2),
    delivery_time INT,
    store_id INT,
    FOREIGN KEY (store_id) REFERENCES public.stores(store_id)
);

CREATE TABLE IF NOT EXISTS public.order_statuses
(
    status_id SERIAL PRIMARY KEY,
    status_name VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS public."Users"
(
    "Id" TEXT PRIMARY KEY,
    "Address" VARCHAR(256),
    "UserName" VARCHAR(256),
    "NormalizedUserName" VARCHAR(256) UNIQUE,
    "Email" VARCHAR(256),
    "NormalizedEmail" VARCHAR(256),
    "EmailConfirmed" BOOLEAN,
    "PasswordHash" TEXT,
    "SecurityStamp" TEXT,
    "ConcurrencyStamp" TEXT,
    "PhoneNumber" TEXT,
    "PhoneNumberConfirmed" BOOLEAN,
    "TwoFactorEnabled" BOOLEAN,
    "LockoutEnd" TIMESTAMPTZ,
    "LockoutEnabled" BOOLEAN,
    "AccessFailedCount" INT,
    birthday_date TIMESTAMP
);


CREATE TABLE IF NOT EXISTS public.user_cart
(
    cart_id SERIAL PRIMARY KEY,
    product_id INT,
    quantity INT,
    store_id INT,
    user_id TEXT,
    price NUMERIC,
    is_ordered BOOLEAN,
    FOREIGN KEY (product_id) REFERENCES public.products(product_id),
    FOREIGN KEY (store_id) REFERENCES public.stores(store_id),
    FOREIGN KEY (user_id) REFERENCES public."Users"("Id")
);

CREATE TABLE IF NOT EXISTS public.payments
(
    id SERIAL PRIMARY KEY,
    type VARCHAR(255),
    commission NUMERIC(10, 2)
);

CREATE TABLE IF NOT EXISTS public.orders
(
    order_id SERIAL PRIMARY KEY,
    payment_id INT,
    cart_id INT,
    delivery_id INT,
    order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status_id INT,
    FOREIGN KEY (payment_id) REFERENCES public.payments(id),
    FOREIGN KEY (cart_id) REFERENCES public.user_cart(cart_id),
    FOREIGN KEY (delivery_id) REFERENCES public.delivery_options(delivery_id),
    FOREIGN KEY (status_id) REFERENCES public.order_statuses(status_id)
);

CREATE TABLE IF NOT EXISTS public.order_items
(
    item_id SERIAL PRIMARY KEY,
    order_id INT,
    product_price NUMERIC(10, 2),
    total_price NUMERIC(10, 2),
    product_id INT,
    quantity INT,
    store VARCHAR(255),
    FOREIGN KEY (order_id) REFERENCES public.orders(order_id),
    FOREIGN KEY (product_id) REFERENCES public.products(product_id)
);

CREATE TABLE IF NOT EXISTS public."Roles"
(
    "Id" TEXT PRIMARY KEY,
    "Name" TEXT,
    "NormalizedName" TEXT,
    "ConcurrencyStamp" TEXT
);

CREATE TABLE IF NOT EXISTS public."UserRoles"
(
    "UserId" TEXT,
    "RoleId" TEXT,
    PRIMARY KEY ("UserId", "RoleId"),
    FOREIGN KEY ("UserId") REFERENCES public."Users"("Id"),
    FOREIGN KEY ("RoleId") REFERENCES public."Roles"("Id")
);

CREATE TABLE IF NOT EXISTS public."__EFMigrationsHistory"
(
    "MigrationId" VARCHAR(150) PRIMARY KEY,
    "ProductVersion" VARCHAR(32)
);

CREATE TABLE IF NOT EXISTS public."UserClaims"
(
    "Id" SERIAL PRIMARY KEY,
    "UserId" TEXT,
    "ClaimType" TEXT,
    "ClaimValue" TEXT,
    FOREIGN KEY ("UserId") REFERENCES public."Users"("Id")
);

CREATE TABLE IF NOT EXISTS public."UserLogins"
(
    "LoginProvider" TEXT,
    "ProviderKey" TEXT,
    "ProviderDisplayName" TEXT,
    "UserId" TEXT,
    PRIMARY KEY ("LoginProvider", "ProviderKey"),
    FOREIGN KEY ("UserId") REFERENCES public."Users"("Id")
);

CREATE TABLE IF NOT EXISTS public."RoleClaims"
(
    "Id" SERIAL PRIMARY KEY,
    "RoleId" TEXT,
    "ClaimType" TEXT,
    "ClaimValue" TEXT,
    FOREIGN KEY ("RoleId") REFERENCES public."Roles"("Id")
);

CREATE TABLE IF NOT EXISTS public."UserTokens"
(
    "UserId" TEXT,
    "LoginProvider" TEXT,
    "Name" TEXT,
    "Value" TEXT,
    PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    FOREIGN KEY ("UserId") REFERENCES public."Users"("Id")
);