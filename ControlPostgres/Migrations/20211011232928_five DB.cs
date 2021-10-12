using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ControlPostgres.Migrations
{
    public partial class fiveDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_rol_id_tb_empleados_tb_roles",
                schema: "muni_villanueva",
                table: "tb_empleados");

            migrationBuilder.DropTable(
                name: "tb_roles",
                schema: "muni_villanueva");

            migrationBuilder.DropIndex(
                name: "IX_tb_empleados_rol_id",
                schema: "muni_villanueva",
                table: "tb_empleados");

            migrationBuilder.DropColumn(
                name: "rol_id",
                schema: "muni_villanueva",
                table: "tb_empleados");

            migrationBuilder.AddColumn<int>(
                name: "estado_sele_director",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "estado_sele_jefe",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_solicitudes_estado_sele_director",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                column: "estado_sele_director");

            migrationBuilder.CreateIndex(
                name: "IX_tb_solicitudes_estado_sele_jefe",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                column: "estado_sele_jefe");

            migrationBuilder.AddForeignKey(
                name: "fk_estado_sele_director_tb_estadossolicitudes",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                column: "estado_sele_director",
                principalSchema: "muni_villanueva",
                principalTable: "tb_estadosolicitudes",
                principalColumn: "estados_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_estado_sele_jefe_tb_estadossolicitudes",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                column: "estado_sele_jefe",
                principalSchema: "muni_villanueva",
                principalTable: "tb_estadosolicitudes",
                principalColumn: "estados_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_estado_sele_director_tb_estadossolicitudes",
                schema: "muni_villanueva",
                table: "tb_solicitudes");

            migrationBuilder.DropForeignKey(
                name: "fk_estado_sele_jefe_tb_estadossolicitudes",
                schema: "muni_villanueva",
                table: "tb_solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_tb_solicitudes_estado_sele_director",
                schema: "muni_villanueva",
                table: "tb_solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_tb_solicitudes_estado_sele_jefe",
                schema: "muni_villanueva",
                table: "tb_solicitudes");

            migrationBuilder.DropColumn(
                name: "estado_sele_director",
                schema: "muni_villanueva",
                table: "tb_solicitudes");

            migrationBuilder.DropColumn(
                name: "estado_sele_jefe",
                schema: "muni_villanueva",
                table: "tb_solicitudes");

            migrationBuilder.AddColumn<int>(
                name: "rol_id",
                schema: "muni_villanueva",
                table: "tb_empleados",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tb_roles",
                schema: "muni_villanueva",
                columns: table => new
                {
                    rol_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rol_descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rol_nombre = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tb_roles_pkey", x => x.rol_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_empleados_rol_id",
                schema: "muni_villanueva",
                table: "tb_empleados",
                column: "rol_id");

            migrationBuilder.CreateIndex(
                name: "tb_roles_rol_nombre_key",
                schema: "muni_villanueva",
                table: "tb_roles",
                column: "rol_nombre",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_rol_id_tb_empleados_tb_roles",
                schema: "muni_villanueva",
                table: "tb_empleados",
                column: "rol_id",
                principalSchema: "muni_villanueva",
                principalTable: "tb_roles",
                principalColumn: "rol_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
