using Domain.Enums;

namespace Domain.Entities;

public class LogProduto
{
    public Guid Id { get; set; }
    
    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; }
    
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    
    public TipoOperacao TipoOperacao { get; set; }
    public int QuantidadeAnterior { get; set; }
    public int QuantidadeAtual { get; set; }
    
    public DateTime DataAlteracao { get; set; } = DateTime.Now;
}