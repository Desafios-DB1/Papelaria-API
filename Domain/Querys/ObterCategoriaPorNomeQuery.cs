using Crosscutting.Dtos.Categoria;
using Domain.Interfaces;
using MediatR;

namespace Domain.Querys;

public class ObterCategoriaPorNomeQuery(string nome) : IRequest<CategoriaResponseDto>
{
    public string Nome { get; } = nome;
}

public class ObterCategoriaPorNomeQueryHandler(ICategoriaService service) : IRequestHandler<ObterCategoriaPorNomeQuery, CategoriaResponseDto>
{
    public async Task<CategoriaResponseDto> Handle(ObterCategoriaPorNomeQuery request, CancellationToken cancellationToken)
    {
        return await service.ObterPorNomeAsync(request, cancellationToken);
    }
}