using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tamagotchi.Data.Migrations
{
    public partial class AddedOrganizationsPhotosFileds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "primaryIcon",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "primaryPhoto",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "primaryIcon",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "primaryPhoto",
                table: "Organizations");
        }
    }
}
