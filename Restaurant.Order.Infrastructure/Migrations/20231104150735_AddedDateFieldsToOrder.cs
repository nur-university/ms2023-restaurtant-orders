using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateFieldsToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "acceptedDate",
                table: "order",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "confirmedDate",
                table: "order",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "startedDate",
                table: "order",
                type: "datetime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "acceptedDate",
                table: "order");

            migrationBuilder.DropColumn(
                name: "confirmedDate",
                table: "order");

            migrationBuilder.DropColumn(
                name: "startedDate",
                table: "order");
        }
    }
}
