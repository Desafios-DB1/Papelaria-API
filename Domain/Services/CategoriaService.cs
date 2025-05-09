using Crosscutting.Constantes;
using Crosscutting.Dtos.Categoria;
using Crosscutting.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Mappers;
using Domain.Repositories;

namespace Domain.Services;

public class CategoriaService(ICategoriaRepository repository) :
    CrudServiceBase<Categoria, CategoriaDto>(repository),
    ICategoriaService
{
    public async Task<CategoriaDto> ObterPorNomeAsync(string nome)
    {
        if (string.IsNullOrEmpty(nome))
            throw new RequisicaoInvalidaException(ErrorMessages.CampoNulo("nome"));
        
        var categoria = await repository.ObterPorNomeAsync(nome)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste(Entidades.Categoria));

        return categoria.MapToDto();
    }

    protected override Categoria MapToEntity(CategoriaDto dto)
    {
        return dto.MapToEntity();
    }

    protected override CategoriaDto MapToResponseDto(Categoria entity)
    {
        return entity.MapToDto();
    }
}