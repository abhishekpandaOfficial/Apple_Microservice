using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apple.Services.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNametoAspNetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameOfUser",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameOfUser",
                table: "AspNetUsers");
        }
    }
}
