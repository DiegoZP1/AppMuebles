using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppMuebles.Data.Migrations
{
    public partial class octavaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_resp_contact",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContactoId = table.Column<int>(type: "integer", nullable: false),
                    Respuesta = table.Column<string>(type: "text", nullable: false),
                    RespuestatDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_resp_contact", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_resp_contact_t_contacto_ContactoId",
                        column: x => x.ContactoId,
                        principalTable: "t_contacto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_resp_contact_ContactoId",
                table: "t_resp_contact",
                column: "ContactoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_resp_contact");
        }
    }
}
