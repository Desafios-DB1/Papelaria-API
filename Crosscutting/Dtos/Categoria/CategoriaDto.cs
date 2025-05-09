namespace Crosscutting.Dtos.Categoria;

public class CategoriaDto : IBaseDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; } 
}