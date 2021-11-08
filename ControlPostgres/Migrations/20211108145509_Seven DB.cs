using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlPostgres.Migrations
{
    public partial class SevenDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "periodo_vacas",
                schema: "muni_villanueva",
                table: "tb_solicitudes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "periodo_vacas",
                schema: "muni_villanueva",
                table: "tb_solicitudes");
        }
    }
}
