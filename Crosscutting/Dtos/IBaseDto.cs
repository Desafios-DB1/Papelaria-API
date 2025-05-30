namespace Crosscutting.Dtos;

public interface IBaseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
}