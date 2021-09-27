using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ControlPostgres.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "muni_villanueva");

            migrationBuilder.CreateTable(
                name: "tb_departamentos",
                schema: "muni_villanueva",
                columns: table => new
                {
                    depto_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    depto_nombre = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    depto_descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tb_departamentos_pkey", x => x.depto_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_estadosolicitudes",
                schema: "muni_villanueva",
                columns: table => new
                {
                    estados_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    estados_nombre = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tb_estadosolicitudes_pkey", x => x.estados_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_roles",
                schema: "muni_villanueva",
                columns: table => new
                {
                    rol_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rol_nombre = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    rol_descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tb_roles_pkey", x => x.rol_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_vacaciones",
                schema: "muni_villanueva",
                columns: table => new
                {
                    vacaciones_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vacaciones_estado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    vacaciones_tiempo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tb_vacaciones_pkey", x => x.vacaciones_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_cargos",
                schema: "muni_villanueva",
                columns: table => new
                {
                    cargo_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    depto_id = table.Column<int>(type: "integer", nullable: false),
                    cargo_nombre = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    cargo_descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tb_cargos_pkey", x => x.cargo_id);
                    table.ForeignKey(
                        name: "fk_depto_id_tb_departamentos",
                        column: x => x.depto_id,
                        principalSchema: "muni_villanueva",
                        principalTable: "tb_departamentos",
                        principalColumn: "depto_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_empleados",
                schema: "muni_villanueva",
                columns: table => new
                {
                    empleado_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cargo_id = table.Column<int>(type: "integer", nullable: false),
                    depto_id = table.Column<int>(type: "integer", nullable: false),
                    vacaciones_id = table.Column<int>(type: "integer", nullable: false),
                    rol_id = table.Column<int>(type: "integer", nullable: false),
                    empleado_codigo = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    empleado_contraseña = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    empleado_nombre1 = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    empleado_nombre2 = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    empleado_apellido1 = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    empleado_apellido2 = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    apellido_casada = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    fecha_nacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    empleado_telefono = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    empleado_direccion = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    empleado_estado = table.Column<int>(type: "integer", nullable: false),
                    fecha_ingreso = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tb_empleados_pkey", x => x.empleado_id);
                    table.ForeignKey(
                        name: "fk_cargo_id_tb_emepleados_tb_cargos",
                        column: x => x.cargo_id,
                        principalSchema: "muni_villanueva",
                        principalTable: "tb_cargos",
                        principalColumn: "cargo_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_depto_id_tb_empleados_tb_departamentos",
                        column: x => x.depto_id,
                        principalSchema: "muni_villanueva",
                        principalTable: "tb_departamentos",
                        principalColumn: "depto_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_rol_id_tb_empleados_tb_roles",
                        column: x => x.rol_id,
                        principalSchema: "muni_villanueva",
                        principalTable: "tb_roles",
                        principalColumn: "rol_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_vacaciones_id_tb_empleados_tb_vacaciones",
                        column: x => x.vacaciones_id,
                        principalSchema: "muni_villanueva",
                        principalTable: "tb_vacaciones",
                        principalColumn: "vacaciones_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_solicitudes",
                schema: "muni_villanueva",
                columns: table => new
                {
                    solicitud_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    empleado_id = table.Column<int>(type: "integer", nullable: false),
                    cargo_id = table.Column<int>(type: "integer", nullable: false),
                    depto_id = table.Column<int>(type: "integer", nullable: false),
                    vacaciones_id = table.Column<int>(type: "integer", nullable: false),
                    estados_id = table.Column<int>(type: "integer", nullable: false),
                    solicitud_fecha = table.Column<DateTime>(type: "date", nullable: false),
                    detalles_solicitud = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    fechas_seleccionadas = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    cantidad_dias = table.Column<int>(type: "integer", nullable: false),
                    comentario = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tb_solicitudes_pkey", x => x.solicitud_id);
                    table.ForeignKey(
                        name: "fk_cargo_id_tb_solicitudes_tb_cargos",
                        column: x => x.cargo_id,
                        principalSchema: "muni_villanueva",
                        principalTable: "tb_cargos",
                        principalColumn: "cargo_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_depto_id_tb_solicitudes_tb_departamentos",
                        column: x => x.depto_id,
                        principalSchema: "muni_villanueva",
                        principalTable: "tb_departamentos",
                        principalColumn: "depto_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_empleado_id_tb_solicitudes_tb_empleados",
                        column: x => x.empleado_id,
                        principalSchema: "muni_villanueva",
                        principalTable: "tb_empleados",
                        principalColumn: "empleado_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_estados_id_tb_solicitudes_tb_estadosolicitudes",
                        column: x => x.estados_id,
                        principalSchema: "muni_villanueva",
                        principalTable: "tb_estadosolicitudes",
                        principalColumn: "estados_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_vacaciones_id_tb_solicitudes_tb_vacaciones",
                        column: x => x.vacaciones_id,
                        principalSchema: "muni_villanueva",
                        principalTable: "tb_vacaciones",
                        principalColumn: "vacaciones_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_cargos_depto_id",
                schema: "muni_villanueva",
                table: "tb_cargos",
                column: "depto_id");

            migrationBuilder.CreateIndex(
                name: "tb_departamentos_depto_nombre_key",
                schema: "muni_villanueva",
                table: "tb_departamentos",
                column: "depto_nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_empleados_cargo_id",
                schema: "muni_villanueva",
                table: "tb_empleados",
                column: "cargo_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_empleados_depto_id",
                schema: "muni_villanueva",
                table: "tb_empleados",
                column: "depto_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_empleados_rol_id",
                schema: "muni_villanueva",
                table: "tb_empleados",
                column: "rol_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_empleados_vacaciones_id",
                schema: "muni_villanueva",
                table: "tb_empleados",
                column: "vacaciones_id");

            migrationBuilder.CreateIndex(
                name: "tb_empleados_empleado_codigo_key",
                schema: "muni_villanueva",
                table: "tb_empleados",
                column: "empleado_codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tb_empleados_empleado_telefono_key",
                schema: "muni_villanueva",
                table: "tb_empleados",
                column: "empleado_telefono",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tb_estadosolicitudes_estados_nombre_key",
                schema: "muni_villanueva",
                table: "tb_estadosolicitudes",
                column: "estados_nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tb_roles_rol_nombre_key",
                schema: "muni_villanueva",
                table: "tb_roles",
                column: "rol_nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_solicitudes_cargo_id",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                column: "cargo_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_solicitudes_depto_id",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                column: "depto_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_solicitudes_empleado_id",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                column: "empleado_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_solicitudes_estados_id",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                column: "estados_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_solicitudes_vacaciones_id",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                column: "vacaciones_id");

            migrationBuilder.CreateIndex(
                name: "tb_vacaciones_vacaciones_estado_key",
                schema: "muni_villanueva",
                table: "tb_vacaciones",
                column: "vacaciones_estado",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_solicitudes",
                schema: "muni_villanueva");

            migrationBuilder.DropTable(
                name: "tb_empleados",
                schema: "muni_villanueva");

            migrationBuilder.DropTable(
                name: "tb_estadosolicitudes",
                schema: "muni_villanueva");

            migrationBuilder.DropTable(
                name: "tb_cargos",
                schema: "muni_villanueva");

            migrationBuilder.DropTable(
                name: "tb_roles",
                schema: "muni_villanueva");

            migrationBuilder.DropTable(
                name: "tb_vacaciones",
                schema: "muni_villanueva");

            migrationBuilder.DropTable(
                name: "tb_departamentos",
                schema: "muni_villanueva");
        }
    }
}
