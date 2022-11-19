using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class MakeProductIdAllowNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountRules_Products_ProductId",
                schema: "Catalog",
                table: "DiscountRules");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                schema: "Catalog",
                table: "DiscountRules",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountRules_Products_ProductId",
                schema: "Catalog",
                table: "DiscountRules",
                column: "ProductId",
                principalSchema: "Catalog",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountRules_Products_ProductId",
                schema: "Catalog",
                table: "DiscountRules");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                schema: "Catalog",
                table: "DiscountRules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountRules_Products_ProductId",
                schema: "Catalog",
                table: "DiscountRules",
                column: "ProductId",
                principalSchema: "Catalog",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
