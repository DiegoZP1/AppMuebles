using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMuebles.Data.Migrations
{
    public partial class novenaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "estado",
                table: "t_contacto",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "numero",
                table: "t_contacto",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estado",
                table: "t_contacto");

            migrationBuilder.DropColumn(
                name: "numero",
                table: "t_contacto");
        }
    }
}
