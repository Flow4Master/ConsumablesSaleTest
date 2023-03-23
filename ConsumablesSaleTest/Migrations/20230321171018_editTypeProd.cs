using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsumablesSaleTest.Migrations
{
    public partial class editTypeProd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Types_TypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Products",
                newName: "TypeProdId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_TypeId",
                table: "Products",
                newName: "IX_Products_TypeProdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Types_TypeProdId",
                table: "Products",
                column: "TypeProdId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Types_TypeProdId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TypeProdId",
                table: "Products",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_TypeProdId",
                table: "Products",
                newName: "IX_Products_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Types_TypeId",
                table: "Products",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
