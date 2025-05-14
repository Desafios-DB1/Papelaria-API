using Crosscutting.Enums;

namespace Domain.Entities;

public class LogProduto : Entidade
{
    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; }
    
    public string UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    
    public TipoOperacao TipoOperacao { get; set; }
    public int QuantidadeAnterior { get; set; }
    public int QuantidadeAtual { get; set; }
    
    public DateTime DataAlteracao { get; set; } = DateTime.Now;
    public override void Atualizar<T>(T dto)
    {
        throw new NotSupportedException("Atualização não é suportado para LogProduto.");
    }
}