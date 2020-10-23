using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgenesisProperties.data.Migrations.ApplicationDb
{
    public partial class Addedcolumnstopropertiesentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Properties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhoneNumber",
                table: "Properties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Properties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Properties",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBaths",
                table: "Properties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRooms",
                table: "Properties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfToilets",
                table: "Properties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Properties",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ContactPhoneNumber",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "NumberOfBaths",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "NumberOfRooms",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "NumberOfToilets",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Properties");
        }
    }
}
