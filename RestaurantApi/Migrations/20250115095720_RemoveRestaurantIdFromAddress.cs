using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRestaurantIdFromAddress : Migration
    {
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Addresses");
        }
    }
}