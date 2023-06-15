using Microsoft.EntityFrameworkCore.Migrations;

namespace Graduation_Project2.Migrations
{
    public partial class img : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IDPhoto",
                table: "patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDPhoto",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDPhoto",
                table: "doctorAdmins",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDPhoto",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "IDPhoto",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "IDPhoto",
                table: "doctorAdmins");
        }
    }
}
