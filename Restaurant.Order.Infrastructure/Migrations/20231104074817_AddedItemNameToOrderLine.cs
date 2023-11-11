using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedItemNameToOrderLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "itemName",
                table: "orderLine",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "itemName",
                table: "orderLine");
        }
    }
}
