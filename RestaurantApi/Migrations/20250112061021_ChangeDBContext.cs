using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDBContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Restaurans_RestaurantId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurans_Addresses_AddressId",
                table: "Restaurans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurans",
                table: "Restaurans");

            migrationBuilder.RenameTable(
                name: "Restaurans",
                newName: "Restaurants");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurans_AddressId",
                table: "Restaurants",
                newName: "IX_Restaurants_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Restaurants_RestaurantId",
                table: "Dishes",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Addresses_AddressId",
                table: "Restaurants",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Restaurants_RestaurantId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Addresses_AddressId",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants");

            migrationBuilder.RenameTable(
                name: "Restaurants",
                newName: "Restaurans");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_AddressId",
                table: "Restaurans",
                newName: "IX_Restaurans_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurans",
                table: "Restaurans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Restaurans_RestaurantId",
                table: "Dishes",
                column: "RestaurantId",
                principalTable: "Restaurans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurans_Addresses_AddressId",
                table: "Restaurans",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}