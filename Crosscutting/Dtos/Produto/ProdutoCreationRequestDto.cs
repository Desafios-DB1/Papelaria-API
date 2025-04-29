
namespace Crosscutting.Dtos.Produto;

public class ProdutoCreationRequestDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public Guid CategoriaId { get; set; }
    public int QuantidadeMinima { get; set; }
    public int QuantidadeAtual { get; set; }
    public decimal PrecoCompra { get; set; }
    public decimal PrecoVenda { get; set; }
}
