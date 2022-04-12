using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tamagotchi.Data.Migrations
{
    public partial class AddAnimalsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    organizationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    species = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    primaryBreed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    secondaryBreed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isMixedBreed = table.Column<bool>(type: "bit", nullable: false),
                    isUnknownBreed = table.Column<bool>(type: "bit", nullable: false),
                    primaryColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    secondaryColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tertiaryColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    coat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    attributeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    childrenEnvinronment = table.Column<bool>(type: "bit", nullable: true),
                    dogsEnvinronment = table.Column<bool>(type: "bit", nullable: true),
                    catsEnvinronment = table.Column<bool>(type: "bit", nullable: true),
                    tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    organizationAnimalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    photos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    primaryPhoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    primaryIcon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    videos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status_changed_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    published_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");
        }
    }
}
