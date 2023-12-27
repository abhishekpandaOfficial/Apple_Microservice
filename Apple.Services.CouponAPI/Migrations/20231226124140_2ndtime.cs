using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Apple.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class _2ndtime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CouponCde",
                table: "Coupons",
                newName: "CouponCode");

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "CreatedDateTime", "DiscountAmount", "MinAmount" },
                values: new object[,]
                {
                    { 1, "10OFF", new DateTime(2023, 12, 26, 18, 11, 39, 847, DateTimeKind.Local).AddTicks(3653), 10.0, 10 },
                    { 2, "20OFF", new DateTime(2023, 12, 26, 18, 11, 39, 847, DateTimeKind.Local).AddTicks(3696), 20.0, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "CouponCode",
                table: "Coupons",
                newName: "CouponCde");
        }
    }
}
