using Microsoft.EntityFrameworkCore.Migrations;

namespace Graduation_Project2.Migrations
{
    public partial class one_manyRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorAdminId",
                table: "schedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicalCentreId",
                table: "Guides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicalCentreId",
                table: "doctorAdmins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GuideId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_schedules_DoctorAdminId",
                table: "schedules",
                column: "DoctorAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_MedicalCentreId",
                table: "Guides",
                column: "MedicalCentreId");

            migrationBuilder.CreateIndex(
                name: "IX_doctorAdmins_MedicalCentreId",
                table: "doctorAdmins",
                column: "MedicalCentreId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_GuideId",
                table: "Appointments",
                column: "GuideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Guides_GuideId",
                table: "Appointments",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "GuideId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_doctorAdmins_medicalCentres_MedicalCentreId",
                table: "doctorAdmins",
                column: "MedicalCentreId",
                principalTable: "medicalCentres",
                principalColumn: "MedicalCentreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Guides_medicalCentres_MedicalCentreId",
                table: "Guides",
                column: "MedicalCentreId",
                principalTable: "medicalCentres",
                principalColumn: "MedicalCentreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_doctorAdmins_DoctorAdminId",
                table: "schedules",
                column: "DoctorAdminId",
                principalTable: "doctorAdmins",
                principalColumn: "DoctorAdminId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Guides_GuideId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_doctorAdmins_medicalCentres_MedicalCentreId",
                table: "doctorAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_Guides_medicalCentres_MedicalCentreId",
                table: "Guides");

            migrationBuilder.DropForeignKey(
                name: "FK_schedules_doctorAdmins_DoctorAdminId",
                table: "schedules");

            migrationBuilder.DropIndex(
                name: "IX_schedules_DoctorAdminId",
                table: "schedules");

            migrationBuilder.DropIndex(
                name: "IX_Guides_MedicalCentreId",
                table: "Guides");

            migrationBuilder.DropIndex(
                name: "IX_doctorAdmins_MedicalCentreId",
                table: "doctorAdmins");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_GuideId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorAdminId",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "MedicalCentreId",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "MedicalCentreId",
                table: "doctorAdmins");

            migrationBuilder.DropColumn(
                name: "GuideId",
                table: "Appointments");
        }
    }
}
