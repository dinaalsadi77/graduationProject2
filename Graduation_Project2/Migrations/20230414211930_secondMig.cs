using Microsoft.EntityFrameworkCore.Migrations;

namespace Graduation_Project2.Migrations
{
    public partial class secondMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorAdminId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorAdminId",
                table: "Appointments",
                column: "DoctorAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_doctorAdmins_DoctorAdminId",
                table: "Appointments",
                column: "DoctorAdminId",
                principalTable: "doctorAdmins",
                principalColumn: "DoctorAdminId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_doctorAdmins_DoctorAdminId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorAdminId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorAdminId",
                table: "Appointments");
        }
    }
}
