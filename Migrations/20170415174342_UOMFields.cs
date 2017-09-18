using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stockzilla.Migrations
{
    public partial class UOMFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UOMId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UOMs",
                columns: table => new
                {
                    UOMId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SiteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UOMs", x => x.UOMId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UOMId",
                table: "Products",
                column: "UOMId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_UOMs_UOMId",
                table: "Products",
                column: "UOMId",
                principalTable: "UOMs",
                principalColumn: "UOMId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_UOMs_UOMId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "UOMs");

            migrationBuilder.DropIndex(
                name: "IX_Products_UOMId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UOMId",
                table: "Products");
        }
    }
}
