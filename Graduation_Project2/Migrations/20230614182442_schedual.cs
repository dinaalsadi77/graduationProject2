using Microsoft.EntityFrameworkCore.Migrations;

namespace Graduation_Project2.Migrations
{
    public partial class schedual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedules_doctorAdmins_doctorAdminId",
                table: "schedules");

            migrationBuilder.RenameColumn(
                name: "doctorAdminId",
                table: "schedules",
                newName: "AllScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_schedules_doctorAdminId",
                table: "schedules",
                newName: "IX_schedules_AllScheduleId");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorAdminId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "scheduleId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "allSchedules",
                columns: table => new
                {
                    AllScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctorAdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allSchedules", x => x.AllScheduleId);
                    table.ForeignKey(
                        name: "FK_allSchedules_doctorAdmins_doctorAdminId",
                        column: x => x.doctorAdminId,
                        principalTable: "doctorAdmins",
                        principalColumn: "DoctorAdminId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_scheduleId",
                table: "Appointments",
                column: "scheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_allSchedules_doctorAdminId",
                table: "allSchedules",
                column: "doctorAdminId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_schedules_scheduleId",
                table: "Appointments",
                column: "scheduleId",
                principalTable: "schedules",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_allSchedules_AllScheduleId",
                table: "schedules",
                column: "AllScheduleId",
                principalTable: "allSchedules",
                principalColumn: "AllScheduleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_schedules_scheduleId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_schedules_allSchedules_AllScheduleId",
                table: "schedules");

            migrationBuilder.DropTable(
                name: "allSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_scheduleId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "scheduleId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "AllScheduleId",
                table: "schedules",
                newName: "doctorAdminId");

            migrationBuilder.RenameIndex(
                name: "IX_schedules_AllScheduleId",
                table: "schedules",
                newName: "IX_schedules_doctorAdminId");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorAdminId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_doctorAdmins_doctorAdminId",
                table: "schedules",
                column: "doctorAdminId",
                principalTable: "doctorAdmins",
                principalColumn: "DoctorAdminId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
