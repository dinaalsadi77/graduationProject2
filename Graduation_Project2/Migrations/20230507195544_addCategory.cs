using Microsoft.EntityFrameworkCore.Migrations;

namespace Graduation_Project2.Migrations
{
    public partial class addCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cetegory",
                table: "patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cetegory",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cetegory",
                table: "doctorAdmins",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cetegory",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "Cetegory",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "Cetegory",
                table: "doctorAdmins");
        }
    }
}
