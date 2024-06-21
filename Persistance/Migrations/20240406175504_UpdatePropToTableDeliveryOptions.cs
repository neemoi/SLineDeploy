using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropToTableDeliveryOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "options",
                newName: "delivery_type");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "options",
                newName: "delivery_time");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "options",
                newName: "delivery_price");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "options",
                newName: "delivery_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "delivery_type",
                table: "options",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "delivery_time",
                table: "options",
                newName: "time");

            migrationBuilder.RenameColumn(
                name: "delivery_price",
                table: "options",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "delivery_id",
                table: "options",
                newName: "id");
        }
    }
}
