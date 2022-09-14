using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppMuebles.Data.Migrations
{
    public partial class sextaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_order_detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductoId = table.Column<int>(type: "integer", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric", nullable: false),
                    pedidoID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_order_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_order_detail_t_muebles_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "t_muebles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_order_detail_t_order_pedidoID",
                        column: x => x.pedidoID,
                        principalTable: "t_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_order_detail_pedidoID",
                table: "t_order_detail",
                column: "pedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_t_order_detail_ProductoId",
                table: "t_order_detail",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_order_detail");
        }
    }
}
