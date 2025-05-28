using Crosscutting.Enums;

namespace Crosscutting.Dtos.Log;

public class LogDto
{
    public string NomeProduto { get; set; }
    public string NomeUsuarioResponsavel { get; set; }
    public TipoOperacao TipoOperacao { get; set; }
    public int QuantidadeAnterior { get; set; }
    public int NovaQuantidade { get; set; }
    public DateTime DataHora { get; set; }
}