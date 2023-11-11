using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    orderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    clientName = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    orderDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.orderId);
                });

            migrationBuilder.CreateTable(
                name: "outboxMessage",
                columns: table => new
                {
                    outboxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    processed = table.Column<bool>(type: "bit", nullable: false),
                    processedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outboxMessage", x => x.outboxId);
                });

            migrationBuilder.CreateTable(
                name: "orderLine",
                columns: table => new
                {
                    orderLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    itemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unitaryPrice = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    subTotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    orderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderLine", x => x.orderLineId);
                    table.ForeignKey(
                        name: "FK_orderLine_order_orderId",
                        column: x => x.orderId,
                        principalTable: "order",
                        principalColumn: "orderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderLine_orderId",
                table: "orderLine",
                column: "orderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderLine");

            migrationBuilder.DropTable(
                name: "outboxMessage");

            migrationBuilder.DropTable(
                name: "order");
        }
    }
}
