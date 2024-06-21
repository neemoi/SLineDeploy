using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddPropToTableDeliveryOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "delivery_options",
                newName: "options");

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

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "options",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "options");

            migrationBuilder.RenameTable(
                name: "options",
                newName: "delivery_options");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "delivery_options",
                newName: "delivery_time");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "delivery_options",
                newName: "delivery_price");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "delivery_options",
                newName: "delivery_id");
        }
    }
}
