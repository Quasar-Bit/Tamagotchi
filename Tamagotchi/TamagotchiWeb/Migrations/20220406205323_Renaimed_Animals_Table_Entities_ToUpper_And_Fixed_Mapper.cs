using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TamagotchiWeb.Migrations
{
    public partial class Renaimed_Animals_Table_Entities_ToUpper_And_Fixed_Mapper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "videos",
                table: "Animals",
                newName: "Videos");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "Animals",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Animals",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "tertiaryColor",
                table: "Animals",
                newName: "TertiaryColor");

            migrationBuilder.RenameColumn(
                name: "tags",
                table: "Animals",
                newName: "Tags");

            migrationBuilder.RenameColumn(
                name: "status_changed_at",
                table: "Animals",
                newName: "Status_changed_at");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Animals",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "Animals",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "species",
                table: "Animals",
                newName: "Species");

            migrationBuilder.RenameColumn(
                name: "specialNeeds",
                table: "Animals",
                newName: "SpecialNeeds");

            migrationBuilder.RenameColumn(
                name: "spayedNeutered",
                table: "Animals",
                newName: "SpayedNeutered");

            migrationBuilder.RenameColumn(
                name: "size",
                table: "Animals",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "shotsCurrent",
                table: "Animals",
                newName: "ShotsCurrent");

            migrationBuilder.RenameColumn(
                name: "secondaryColor",
                table: "Animals",
                newName: "SecondaryColor");

            migrationBuilder.RenameColumn(
                name: "secondaryBreed",
                table: "Animals",
                newName: "SecondaryBreed");

            migrationBuilder.RenameColumn(
                name: "published_at",
                table: "Animals",
                newName: "Published_at");

            migrationBuilder.RenameColumn(
                name: "primaryPhoto",
                table: "Animals",
                newName: "PrimaryPhoto");

            migrationBuilder.RenameColumn(
                name: "primaryIcon",
                table: "Animals",
                newName: "PrimaryIcon");

            migrationBuilder.RenameColumn(
                name: "primaryColor",
                table: "Animals",
                newName: "PrimaryColor");

            migrationBuilder.RenameColumn(
                name: "primaryBreed",
                table: "Animals",
                newName: "PrimaryBreed");

            migrationBuilder.RenameColumn(
                name: "postcode",
                table: "Animals",
                newName: "Postcode");

            migrationBuilder.RenameColumn(
                name: "photos",
                table: "Animals",
                newName: "Photos");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Animals",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "organizationId",
                table: "Animals",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "organizationAnimalId",
                table: "Animals",
                newName: "OrganizationAnimalId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Animals",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "isUnknownBreed",
                table: "Animals",
                newName: "IsUnknownBreed");

            migrationBuilder.RenameColumn(
                name: "isMixedBreed",
                table: "Animals",
                newName: "IsMixedBreed");

            migrationBuilder.RenameColumn(
                name: "houseTrained",
                table: "Animals",
                newName: "HouseTrained");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Animals",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Animals",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "dogsEnvinronment",
                table: "Animals",
                newName: "DogsEnvinronment");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Animals",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "declawed",
                table: "Animals",
                newName: "Declawed");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Animals",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "coat",
                table: "Animals",
                newName: "Coat");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Animals",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "childrenEnvinronment",
                table: "Animals",
                newName: "ChildrenEnvinronment");

            migrationBuilder.RenameColumn(
                name: "catsEnvinronment",
                table: "Animals",
                newName: "CatsEnvinronment");

            migrationBuilder.RenameColumn(
                name: "animalId",
                table: "Animals",
                newName: "AnimalId");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "Animals",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "address2",
                table: "Animals",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "address1",
                table: "Animals",
                newName: "Address1");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Animals",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Videos",
                table: "Animals",
                newName: "videos");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Animals",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Animals",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "TertiaryColor",
                table: "Animals",
                newName: "tertiaryColor");

            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "Animals",
                newName: "tags");

            migrationBuilder.RenameColumn(
                name: "Status_changed_at",
                table: "Animals",
                newName: "status_changed_at");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Animals",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Animals",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "Species",
                table: "Animals",
                newName: "species");

            migrationBuilder.RenameColumn(
                name: "SpecialNeeds",
                table: "Animals",
                newName: "specialNeeds");

            migrationBuilder.RenameColumn(
                name: "SpayedNeutered",
                table: "Animals",
                newName: "spayedNeutered");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Animals",
                newName: "size");

            migrationBuilder.RenameColumn(
                name: "ShotsCurrent",
                table: "Animals",
                newName: "shotsCurrent");

            migrationBuilder.RenameColumn(
                name: "SecondaryColor",
                table: "Animals",
                newName: "secondaryColor");

            migrationBuilder.RenameColumn(
                name: "SecondaryBreed",
                table: "Animals",
                newName: "secondaryBreed");

            migrationBuilder.RenameColumn(
                name: "Published_at",
                table: "Animals",
                newName: "published_at");

            migrationBuilder.RenameColumn(
                name: "PrimaryPhoto",
                table: "Animals",
                newName: "primaryPhoto");

            migrationBuilder.RenameColumn(
                name: "PrimaryIcon",
                table: "Animals",
                newName: "primaryIcon");

            migrationBuilder.RenameColumn(
                name: "PrimaryColor",
                table: "Animals",
                newName: "primaryColor");

            migrationBuilder.RenameColumn(
                name: "PrimaryBreed",
                table: "Animals",
                newName: "primaryBreed");

            migrationBuilder.RenameColumn(
                name: "Postcode",
                table: "Animals",
                newName: "postcode");

            migrationBuilder.RenameColumn(
                name: "Photos",
                table: "Animals",
                newName: "photos");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Animals",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Animals",
                newName: "organizationId");

            migrationBuilder.RenameColumn(
                name: "OrganizationAnimalId",
                table: "Animals",
                newName: "organizationAnimalId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Animals",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "IsUnknownBreed",
                table: "Animals",
                newName: "isUnknownBreed");

            migrationBuilder.RenameColumn(
                name: "IsMixedBreed",
                table: "Animals",
                newName: "isMixedBreed");

            migrationBuilder.RenameColumn(
                name: "HouseTrained",
                table: "Animals",
                newName: "houseTrained");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Animals",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Animals",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "DogsEnvinronment",
                table: "Animals",
                newName: "dogsEnvinronment");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Animals",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Declawed",
                table: "Animals",
                newName: "declawed");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Animals",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "Coat",
                table: "Animals",
                newName: "coat");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Animals",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "ChildrenEnvinronment",
                table: "Animals",
                newName: "childrenEnvinronment");

            migrationBuilder.RenameColumn(
                name: "CatsEnvinronment",
                table: "Animals",
                newName: "catsEnvinronment");

            migrationBuilder.RenameColumn(
                name: "AnimalId",
                table: "Animals",
                newName: "animalId");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Animals",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "Animals",
                newName: "address2");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "Animals",
                newName: "address1");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Animals",
                newName: "id");
        }
    }
}
