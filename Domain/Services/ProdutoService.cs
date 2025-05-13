using Crosscutting.Dtos.Produto;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Mappers;
using Domain.Repositories;

namespace Domain.Services;

public class ProdutoService(IProdutoRepository repository) :
    CrudServiceBase<Produto, ProdutoDto>(repository),
    IProdutoService
{
    protected override Produto MapToEntity(ProdutoDto dto)
    {
        return dto.MapToEntity();
    }

    protected override ProdutoDto MapToResponseDto(Produto entity)
    {
        return entity.MapToDto();
    }
}