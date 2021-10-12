using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlPostgres.Migrations
{
    public partial class SixDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "estado_sele_jefe",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "estado_sele_director",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "estado_sele_jefe",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "estado_sele_director",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

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
    }
}
