using Crosscutting.Constantes;
using Crosscutting.Dtos.Categoria;
using Crosscutting.Exceptions;
using Domain.Interfaces;
using Domain.Mappers;
using Domain.Repositories;
using FluentValidation;

namespace Domain.Services;

public class CategoriaService(ICategoriaRepository repository) : ICategoriaService
{
    public async Task<Guid> CriarAsync(CategoriaCreationRequestDto categoriaDto)
    {
        if (categoriaDto is null)
            throw new RequisicaoInvalidaException(ErrorMessages.DtoNulo("categoria"));
        
        var categoria = categoriaDto.MapToEntity();
        return await repository.AdicionarESalvarAsync(categoria);
    }
}