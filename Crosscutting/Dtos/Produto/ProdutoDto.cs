using Crosscutting.Dtos;

namespace Crosscutting.Dtos.Produto;

public class ProdutoDto : IBaseDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; }
    public decimal PrecoCompra { get; set; }
}