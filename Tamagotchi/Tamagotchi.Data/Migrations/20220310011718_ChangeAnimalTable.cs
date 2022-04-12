using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tamagotchi.Data.Migrations
{
    public partial class ChangeAnimalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "attributeId",
                table: "Animals");

            migrationBuilder.AddColumn<bool>(
                name: "declawed",
                table: "Animals",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "houseTrained",
                table: "Animals",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "shotsCurrent",
                table: "Animals",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "spayedNeutered",
                table: "Animals",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "specialNeeds",
                table: "Animals",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "declawed",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "houseTrained",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "shotsCurrent",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "spayedNeutered",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "specialNeeds",
                table: "Animals");

            migrationBuilder.AddColumn<string>(
                name: "attributeId",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
