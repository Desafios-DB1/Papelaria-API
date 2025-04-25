using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class Usuario : IdentityUser
{
    public string NomeUsuario { get; set; }
    public string NomeCompleto { get; set; }
    public DateTime DataCriacao { get; set; }

    public ICollection<LogProduto> Logs { get; set; } = new List<LogProduto>();
}