using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stockzilla.Migrations
{
    public partial class StockReceipts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockReceipts",
                columns: table => new
                {
                    StockReceiptId = table.Column<Guid>(nullable: false),
                    BatchNo = table.Column<string>(maxLength: 100, nullable: true),
                    CostPerUOM = table.Column<decimal>(nullable: false),
                    DateReceived = table.Column<DateTime>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    Qty = table.Column<decimal>(nullable: false),
                    ReceivedBy = table.Column<string>(maxLength: 100, nullable: false),
                    SerialNo = table.Column<string>(maxLength: 100, nullable: true),
                    SiteId = table.Column<Guid>(nullable: false),
                    TotalCost = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceipts", x => x.StockReceiptId);
                    table.ForeignKey(
                        name: "FK_StockReceipts_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockReceipts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipts_LocationId",
                table: "StockReceipts",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipts_ProductId",
                table: "StockReceipts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockReceipts");
        }
    }
}
