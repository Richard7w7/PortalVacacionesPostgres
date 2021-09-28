using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlPostgres.Migrations
{
    public partial class ThirdDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "emp_diasvacaciones",
                schema: "muni_villanueva",
                table: "tb_empleados",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emp_diasvacaciones",
                schema: "muni_villanueva",
                table: "tb_empleados");
        }
    }
}
