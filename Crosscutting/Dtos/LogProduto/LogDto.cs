using Crosscutting.Enums;

namespace Crosscutting.Dtos.LogProduto;

public class LogDto
{
    public Guid ProdutoId { get; set; }
    public string NomeProduto { get; set; }
    public string UsuarioId { get; set; }
    public string NomeUsuario { get; set; }
    public TipoOperacao TipoOperacao { get; set; }
    public int QuantidadeAnterior { get; set; }
    public int QuantidadeAtual { get; set; }
    public DateTime DataOperacao { get; set; }
}