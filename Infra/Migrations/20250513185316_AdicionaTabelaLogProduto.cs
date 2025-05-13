using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaTabelaLogProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogsProduto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TipoOperacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantidadeAnterior = table.Column<int>(type: "int", nullable: false),
                    QuantidadeAtual = table.Column<int>(type: "int", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogsProduto_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogsProduto_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogsProduto_ProdutoId",
                table: "LogsProduto",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_LogsProduto_UsuarioId",
                table: "LogsProduto",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogsProduto");
        }
    }
}
