using Crosscutting.Dtos;

namespace Domain.Commands;

public interface IAtualizarCommand : IBaseDto
{
    Guid Id { get; }
}