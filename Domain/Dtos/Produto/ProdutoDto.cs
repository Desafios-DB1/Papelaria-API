using Crosscutting.Dtos;
using Domain.Enums;

namespace Domain.Dtos.Produto;

public class ProdutoDto : IBaseDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; }
    public decimal PrecoCompra { get; set; }
    public decimal PrecoVenda { get; set; }
    
    public Guid CategoriaId { get; set; }
    public string CategoriaNome { get; set; }
    
    public int QuantidadeMinima { get; set; }
    public int QuantidadeAtual { get; set; }
    public StatusEstoque StatusEstoque { get; set; }
}