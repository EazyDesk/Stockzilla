using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stockzilla.Migrations
{
    public partial class FixProductCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Product_Code",
                table: "Products",
                newName: "ProductCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductCode",
                table: "Products",
                newName: "Product_Code");
        }
    }
}
