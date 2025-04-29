using System.ComponentModel.DataAnnotations.Schema;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Produto : Entidade
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public QuantidadeEstoque QuantidadeEstoque {get; set; }
    public decimal PrecoCompra { get; set; }
    public decimal PrecoVenda { get; set; }
    
    [NotMapped]
    public Guid CategoriaId { get; set; }
    
    [NotMapped]
    public Categoria Categoria { get; set; }
    [NotMapped]
    public ICollection<LogProduto> Logs { get; set; } = new List<LogProduto>();
    
    public Produto() {}
}
