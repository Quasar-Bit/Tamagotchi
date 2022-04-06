using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TamagotchiWeb.Migrations
{
    public partial class TestMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "AnimalTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "genders",
                table: "AnimalTypes",
                newName: "Genders");

            migrationBuilder.RenameColumn(
                name: "colors",
                table: "AnimalTypes",
                newName: "Colors");

            migrationBuilder.RenameColumn(
                name: "coats",
                table: "AnimalTypes",
                newName: "Coats");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AnimalTypes",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AnimalTypes",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Genders",
                table: "AnimalTypes",
                newName: "genders");

            migrationBuilder.RenameColumn(
                name: "Colors",
                table: "AnimalTypes",
                newName: "colors");

            migrationBuilder.RenameColumn(
                name: "Coats",
                table: "AnimalTypes",
                newName: "coats");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AnimalTypes",
                newName: "id");
        }
    }
}
