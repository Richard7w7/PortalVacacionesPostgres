using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlPostgres.Migrations
{
    public partial class fourDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "emp_diasvacaciones",
                schema: "muni_villanueva",
                table: "tb_empleados",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "emp_diasvacaciones",
                schema: "muni_villanueva",
                table: "tb_empleados",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
