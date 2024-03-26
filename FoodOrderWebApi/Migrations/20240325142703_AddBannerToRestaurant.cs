using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrderWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddBannerToRestaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BannerName",
                table: "Restaurants",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerName",
                table: "Restaurants");
        }
    }
}
