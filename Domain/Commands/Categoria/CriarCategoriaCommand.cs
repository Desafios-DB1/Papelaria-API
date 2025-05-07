using Domain.Interfaces;
using MediatR;

namespace Domain.Commands.Categoria;

public class CriarCategoriaCommand : IRequest<Guid>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
}

public class CriarCategoriaCommandHandler(
    ICategoriaService categoriaService) : IRequestHandler<CriarCategoriaCommand, Guid>
{
    public async Task<Guid> Handle(CriarCategoriaCommand request, CancellationToken cancellationToken)
    {
        var result = await categoriaService.CriarAsync(request, cancellationToken);
        return result;
    }
}