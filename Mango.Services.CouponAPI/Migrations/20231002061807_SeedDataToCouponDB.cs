using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataToCouponDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "CreatedAt", "DiscountAmount", "MinAmount", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3c176427-f790-4315-bf86-32bbcb3c5a71"), "20OFF", new DateTime(2023, 10, 2, 11, 48, 7, 67, DateTimeKind.Local).AddTicks(3665), 20.0, 40, new DateTime(2023, 10, 2, 11, 48, 7, 67, DateTimeKind.Local).AddTicks(3665) },
                    { new Guid("aa66e99a-80c6-4278-b792-52889b13dd72"), "10OFF", new DateTime(2023, 10, 2, 11, 48, 7, 67, DateTimeKind.Local).AddTicks(3592), 10.0, 20, new DateTime(2023, 10, 2, 11, 48, 7, 67, DateTimeKind.Local).AddTicks(3603) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: new Guid("3c176427-f790-4315-bf86-32bbcb3c5a71"));

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: new Guid("aa66e99a-80c6-4278-b792-52889b13dd72"));
        }
    }
}
