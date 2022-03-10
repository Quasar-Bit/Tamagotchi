using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TamagotchiWeb.Migrations
{
    public partial class changeAnimalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "animalId",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "animalId",
                table: "Animals");
        }
    }
}
