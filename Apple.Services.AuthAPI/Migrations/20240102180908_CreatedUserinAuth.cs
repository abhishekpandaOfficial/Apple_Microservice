using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apple.Services.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreatedUserinAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUser",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "AspNetUsers");
        }
    }
}
