using Microsoft.EntityFrameworkCore.Migrations;

namespace Graduation_Project2.Migrations
{
    public partial class newColInREQ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "registrationReqs");

            migrationBuilder.RenameColumn(
                name: "userID",
                table: "registrationReqs",
                newName: "MedicalCentreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MedicalCentreID",
                table: "registrationReqs",
                newName: "userID");

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "registrationReqs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
