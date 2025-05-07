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

    public async Task RemoverAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new RequisicaoInvalidaException(ErrorMessages.IdNulo("categoria"));
        
        var categoria = await repository.ObterPorIdAsync(id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste("categoria"));
        
        await repository.RemoverESalvarAsync(categoria);
    }
}