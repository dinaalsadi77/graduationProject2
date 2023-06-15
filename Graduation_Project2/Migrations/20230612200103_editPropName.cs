using Microsoft.EntityFrameworkCore.Migrations;

namespace Graduation_Project2.Migrations
{
    public partial class editPropName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GaideId",
                table: "Appointments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GaideId",
                table: "Appointments",
                type: "int",
                nullable: true);
        }
    }
}
