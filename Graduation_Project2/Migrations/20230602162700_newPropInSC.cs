using Microsoft.EntityFrameworkCore.Migrations;

namespace Graduation_Project2.Migrations
{
    public partial class newPropInSC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedules_doctorAdmins_DoctorAdminId",
                table: "schedules");

            migrationBuilder.RenameColumn(
                name: "DoctorAdminId",
                table: "schedules",
                newName: "doctorAdminId");

            migrationBuilder.RenameIndex(
                name: "IX_schedules_DoctorAdminId",
                table: "schedules",
                newName: "IX_schedules_doctorAdminId");

            migrationBuilder.AlterColumn<int>(
                name: "doctorAdminId",
                table: "schedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_doctorAdmins_doctorAdminId",
                table: "schedules",
                column: "doctorAdminId",
                principalTable: "doctorAdmins",
                principalColumn: "DoctorAdminId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedules_doctorAdmins_doctorAdminId",
                table: "schedules");

            migrationBuilder.RenameColumn(
                name: "doctorAdminId",
                table: "schedules",
                newName: "DoctorAdminId");

            migrationBuilder.RenameIndex(
                name: "IX_schedules_doctorAdminId",
                table: "schedules",
                newName: "IX_schedules_DoctorAdminId");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorAdminId",
                table: "schedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_doctorAdmins_DoctorAdminId",
                table: "schedules",
                column: "DoctorAdminId",
                principalTable: "doctorAdmins",
                principalColumn: "DoctorAdminId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
