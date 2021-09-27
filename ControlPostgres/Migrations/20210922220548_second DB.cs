using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlPostgres.Migrations
{
    public partial class secondDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "emplado_ultimavacafin",
                schema: "muni_villanueva",
                table: "tb_empleados",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "empleado_permiso",
                schema: "muni_villanueva",
                table: "tb_empleados",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "empleado_ultimavacainicio",
                schema: "muni_villanueva",
                table: "tb_empleados",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emplado_ultimavacafin",
                schema: "muni_villanueva",
                table: "tb_empleados");

            migrationBuilder.DropColumn(
                name: "empleado_permiso",
                schema: "muni_villanueva",
                table: "tb_empleados");

            migrationBuilder.DropColumn(
                name: "empleado_ultimavacainicio",
                schema: "muni_villanueva",
                table: "tb_empleados");
        }
    }
}
