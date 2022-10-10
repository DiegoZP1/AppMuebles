using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMuebles.Data.Migrations
{
    public partial class model3Dmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "model3D",
                table: "t_muebles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "model3D",
                table: "t_muebles");
        }
    }
}
