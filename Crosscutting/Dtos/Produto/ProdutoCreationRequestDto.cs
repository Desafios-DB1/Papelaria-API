
namespace Crosscutting.Dtos.Produto;

public class ProdutoCreationRequestDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public int QuantidadeMinima { get; set; }
    public int QuantidadeAtual { get; set; }
    public decimal PrecoCompra { get; set; }
    public decimal PrecoVenda { get; set; }
}
