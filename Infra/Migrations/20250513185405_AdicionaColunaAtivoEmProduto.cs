using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaColunaAtivoEmProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Produtos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Produtos");
        }
    }
}
