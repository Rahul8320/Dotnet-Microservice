using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateProducts_SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "CreatedAt", "Description", "ImageUrl", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("13c57be2-7174-4cf1-8cc6-2fe660baed91"), "Appetizer", new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1078), " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", "http://www.cookforindia.com/wp-content/uploads/2016/08/Paneer-Tikka-_LR.jpg", "Paneer Tikka", 13.99, new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1078) },
                    { new Guid("2fde16f1-1eaf-44f7-af35-7e7a49eea0b9"), "Appetizer", new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(991), " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://www.zedamagazine.com/wp-content/uploads/2018/06/Indian-Food-Samosa-Dish-HD-Wallpapers.jpg", "Samosa", 15.0, new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1002) },
                    { new Guid("e4a9e1fb-af06-4f42-a0ea-b4d0fea4dae6"), "Entree", new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1109), " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://i.pinimg.com/originals/2d/37/a6/2d37a6a69d254a42ca1a345304120796.jpg", "Pav Bhaji", 15.0, new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1110) },
                    { new Guid("ff69d67e-1152-4ed0-b206-8360f89cd5f4"), "Dessert", new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1094), " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", "https://img.sndimg.com/food/image/upload/v1/img/recipes/30/76/6/pic0L9jII.jpg", "Sweet Pie", 10.99, new DateTime(2023, 10, 15, 17, 53, 28, 272, DateTimeKind.Local).AddTicks(1095) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
