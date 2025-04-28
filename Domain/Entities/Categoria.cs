namespace Domain.Entities;

public class Categoria : Entidade
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; } = true;
    
    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    
    public Categoria() {}
}