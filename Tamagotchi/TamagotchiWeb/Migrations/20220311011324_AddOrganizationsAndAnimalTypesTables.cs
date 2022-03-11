using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TamagotchiWeb.Migrations
{
    public partial class AddOrganizationsAndAnimalTypesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    colors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    genders = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    organizationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    monday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tuesday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    wednesday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thursday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    friday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    saturday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sunday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mission_statement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adoptionPolicy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adoptionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    youtube = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pinterest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    photos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalTypes");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
