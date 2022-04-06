using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TamagotchiWeb.Migrations
{
    public partial class Renaimed_Organizations_Table_Entities_ToUpper_And_Fixed_Mapper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "youtube",
                table: "Organizations",
                newName: "Youtube");

            migrationBuilder.RenameColumn(
                name: "wednesday",
                table: "Organizations",
                newName: "Wednesday");

            migrationBuilder.RenameColumn(
                name: "website",
                table: "Organizations",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "Organizations",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "twitter",
                table: "Organizations",
                newName: "Twitter");

            migrationBuilder.RenameColumn(
                name: "tuesday",
                table: "Organizations",
                newName: "Tuesday");

            migrationBuilder.RenameColumn(
                name: "thursday",
                table: "Organizations",
                newName: "Thursday");

            migrationBuilder.RenameColumn(
                name: "sunday",
                table: "Organizations",
                newName: "Sunday");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "Organizations",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "saturday",
                table: "Organizations",
                newName: "Saturday");

            migrationBuilder.RenameColumn(
                name: "primaryPhoto",
                table: "Organizations",
                newName: "PrimaryPhoto");

            migrationBuilder.RenameColumn(
                name: "primaryIcon",
                table: "Organizations",
                newName: "PrimaryIcon");

            migrationBuilder.RenameColumn(
                name: "postcode",
                table: "Organizations",
                newName: "Postcode");

            migrationBuilder.RenameColumn(
                name: "pinterest",
                table: "Organizations",
                newName: "Pinterest");

            migrationBuilder.RenameColumn(
                name: "photos",
                table: "Organizations",
                newName: "Photos");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Organizations",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "organizationId",
                table: "Organizations",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Organizations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "monday",
                table: "Organizations",
                newName: "Monday");

            migrationBuilder.RenameColumn(
                name: "mission_statement",
                table: "Organizations",
                newName: "Mission_Statement");

            migrationBuilder.RenameColumn(
                name: "instagram",
                table: "Organizations",
                newName: "Instagram");

            migrationBuilder.RenameColumn(
                name: "friday",
                table: "Organizations",
                newName: "Friday");

            migrationBuilder.RenameColumn(
                name: "facebook",
                table: "Organizations",
                newName: "Facebook");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Organizations",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Organizations",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Organizations",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "adoptionUrl",
                table: "Organizations",
                newName: "AdoptionUrl");

            migrationBuilder.RenameColumn(
                name: "adoptionPolicy",
                table: "Organizations",
                newName: "AdoptionPolicy");

            migrationBuilder.RenameColumn(
                name: "address2",
                table: "Organizations",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "address1",
                table: "Organizations",
                newName: "Address1");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Organizations",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Youtube",
                table: "Organizations",
                newName: "youtube");

            migrationBuilder.RenameColumn(
                name: "Wednesday",
                table: "Organizations",
                newName: "wednesday");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "Organizations",
                newName: "website");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Organizations",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "Twitter",
                table: "Organizations",
                newName: "twitter");

            migrationBuilder.RenameColumn(
                name: "Tuesday",
                table: "Organizations",
                newName: "tuesday");

            migrationBuilder.RenameColumn(
                name: "Thursday",
                table: "Organizations",
                newName: "thursday");

            migrationBuilder.RenameColumn(
                name: "Sunday",
                table: "Organizations",
                newName: "sunday");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Organizations",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "Saturday",
                table: "Organizations",
                newName: "saturday");

            migrationBuilder.RenameColumn(
                name: "PrimaryPhoto",
                table: "Organizations",
                newName: "primaryPhoto");

            migrationBuilder.RenameColumn(
                name: "PrimaryIcon",
                table: "Organizations",
                newName: "primaryIcon");

            migrationBuilder.RenameColumn(
                name: "Postcode",
                table: "Organizations",
                newName: "postcode");

            migrationBuilder.RenameColumn(
                name: "Pinterest",
                table: "Organizations",
                newName: "pinterest");

            migrationBuilder.RenameColumn(
                name: "Photos",
                table: "Organizations",
                newName: "photos");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Organizations",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Organizations",
                newName: "organizationId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Organizations",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Monday",
                table: "Organizations",
                newName: "monday");

            migrationBuilder.RenameColumn(
                name: "Mission_Statement",
                table: "Organizations",
                newName: "mission_statement");

            migrationBuilder.RenameColumn(
                name: "Instagram",
                table: "Organizations",
                newName: "instagram");

            migrationBuilder.RenameColumn(
                name: "Friday",
                table: "Organizations",
                newName: "friday");

            migrationBuilder.RenameColumn(
                name: "Facebook",
                table: "Organizations",
                newName: "facebook");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Organizations",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Organizations",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Organizations",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "AdoptionUrl",
                table: "Organizations",
                newName: "adoptionUrl");

            migrationBuilder.RenameColumn(
                name: "AdoptionPolicy",
                table: "Organizations",
                newName: "adoptionPolicy");

            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "Organizations",
                newName: "address2");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "Organizations",
                newName: "address1");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Organizations",
                newName: "id");
        }
    }
}
