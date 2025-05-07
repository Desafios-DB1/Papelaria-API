using Crosscutting.Constantes;
using Crosscutting.Dtos.Categoria;
using Crosscutting.Exceptions;
using Domain.Commands.Categoria;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Mappers;
using Domain.Repositories;
using FluentValidation;

namespace Domain.Services;

public class CategoriaService(ICategoriaRepository repository) : ICategoriaService
{
    public async Task<Guid> CriarAsync(CriarCategoriaCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.RequestNula);

        var categoria = request.MapToCategoria();
        return await repository.AdicionarESalvarAsync(categoria);
    }
}