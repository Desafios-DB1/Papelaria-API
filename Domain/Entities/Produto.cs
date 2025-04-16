using Domain.ValueObjects;

namespace Domain.Entities;

public class Produto : Entidade<Produto>
{
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public QuantidadeEstoque QuantidadeEstoque {get; private set; }
    public decimal PrecoCompra { get; set; }
    public decimal PrecoVenda { get; set; }

    public Produto() {}
}
