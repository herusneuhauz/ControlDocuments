using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlDocuments.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    ID_Loja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Loja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.ID_Loja);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    ID_Documento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Loja = table.Column<int>(type: "int", nullable: false),
                    Numero_Documento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_Lancamento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.ID_Documento);
                    table.ForeignKey(
                        name: "FK_Documentos_Lojas_ID_Loja",
                        column: x => x.ID_Loja,
                        principalTable: "Lojas",
                        principalColumn: "ID_Loja",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_ID_Loja",
                table: "Documentos",
                column: "ID_Loja");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Lojas");
        }
    }
}
