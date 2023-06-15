using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Graduation_Project2.Migrations
{
    public partial class removeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patients_cridetPayments_CridetPaymentId",
                table: "patients");

            migrationBuilder.DropTable(
                name: "cridetPayments");

            migrationBuilder.DropIndex(
                name: "IX_patients_CridetPaymentId",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "CridetPaymentId",
                table: "patients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CridetPaymentId",
                table: "patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "cridetPayments",
                columns: table => new
                {
                    CridetPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    Password = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cridetPayments", x => x.CridetPaymentId);
                });

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
    }
}
