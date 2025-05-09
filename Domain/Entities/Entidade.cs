using Crosscutting.Dtos;

namespace Domain.Entities;

public abstract class Entidade
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    public abstract void Atualizar<T>(T dto) where T : IBaseDto;
}
