using Microsoft.EntityFrameworkCore.Migrations;

namespace Graduation_Project2.Migrations
{
    public partial class allRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CridetPaymentId",
                table: "patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_patients_CridetPaymentId",
                table: "patients",
                column: "CridetPaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_patients_cridetPayments_CridetPaymentId",
                table: "patients",
                column: "CridetPaymentId",
                principalTable: "cridetPayments",
                principalColumn: "CridetPaymentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patients_cridetPayments_CridetPaymentId",
                table: "patients");

            migrationBuilder.DropIndex(
                name: "IX_patients_CridetPaymentId",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "CridetPaymentId",
                table: "patients");
        }
    }
}
