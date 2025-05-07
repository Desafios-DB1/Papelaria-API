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

    public async Task<Guid> AtualizarAsync(CategoriaUpdateRequestDto categoriaDto)
    {
        if (categoriaDto is null)
            throw new RequisicaoInvalidaException(ErrorMessages.DtoNulo("categoria"));
        
        var categoriaAntiga = await repository.ObterPorIdAsync(categoriaDto.Id);
        if (categoriaAntiga is null)
            throw new RequisicaoInvalidaException(ErrorMessages.NaoExiste("Categoria"));
        
        categoriaAntiga.Nome = categoriaDto.Nome;
        categoriaAntiga.Descricao = categoriaDto.Descricao;
        categoriaAntiga.Ativo = categoriaDto.Ativo;

        var categoriaId = await repository.AtualizarESalvarAsync(categoriaAntiga);
        return categoriaId;
    }
}