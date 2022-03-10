using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TamagotchiWeb.Migrations
{
    public partial class returnedIdChangesBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "animalId",
                table: "Animals",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "animalId",
                table: "Animals");
        }
    }
}
