using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablePaymentAndAddRowsFromOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "options",
                newName: "delivery_options");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "order_items",
                newName: "total_price");

            migrationBuilder.AddColumn<int>(
                name: "payment_id",
                table: "orders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "product_price",
                table: "order_items",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: true),
                    commission = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("payments_pkey", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_payment_id",
                table: "orders",
                column: "payment_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_order_payment_id",
                table: "orders",
                column: "payment_id",
                principalTable: "payments",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_payment_id",
                table: "orders");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropIndex(
                name: "IX_orders_payment_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "payment_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "product_price",
                table: "order_items");

            migrationBuilder.RenameTable(
                name: "delivery_options",
                newName: "options");

            migrationBuilder.RenameColumn(
                name: "total_price",
                table: "order_items",
                newName: "price");
        }
    }
}
