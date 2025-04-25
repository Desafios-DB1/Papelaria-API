using Domain.ValueObjects;

namespace Domain.Entities;

public class Produto : Entidade<Produto>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public QuantidadeEstoque QuantidadeEstoque {get; set; }
    public decimal PrecoCompra { get; set; }
    public decimal PrecoVenda { get; set; }
    
    public Guid CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
    
    public List<LogProduto> Logs { get; set; } = [];
    
    public Produto() {}
}
