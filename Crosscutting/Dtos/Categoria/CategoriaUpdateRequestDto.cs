namespace Crosscutting.Dtos.Categoria;
public class CategoriaUpdateRequestDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; }
}