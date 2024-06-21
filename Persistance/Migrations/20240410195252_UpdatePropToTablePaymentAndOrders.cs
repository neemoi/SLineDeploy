using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropToTablePaymentAndOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_order_payment_id",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "payments_pkey",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_orders_payment_id",
                table: "orders");

            migrationBuilder.AddPrimaryKey(
                name: "payment_pkey",
                table: "payments",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_payment_id",
                table: "orders",
                column: "payment_id");

            migrationBuilder.AddForeignKey(
                name: "orders_payments_id_fkey",
                table: "orders",
                column: "payment_id",
                principalTable: "payments",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "orders_payments_id_fkey",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "payment_pkey",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_orders_payment_id",
                table: "orders");

            migrationBuilder.AddPrimaryKey(
                name: "payments_pkey",
                table: "payments",
                column: "id");

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
    }
}
