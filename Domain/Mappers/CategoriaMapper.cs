using Crosscutting.Dtos.Categoria;
using Domain.Entities;

namespace Domain.Mappers;

public static class CategoriaMapper
{
    public static CategoriaCreationRequestDto MapToCreationDto(this Categoria categoria)
    {
        return new CategoriaCreationRequestDto
        {
            Nome = categoria.Nome,
            Descricao = categoria.Descricao
        };
    }

    public static Categoria MapToEntity(this CategoriaCreationRequestDto categoriaDto)
    {
        return new Categoria
        {
            Nome = categoriaDto.Nome,
            Descricao = categoriaDto.Descricao,
            Ativo = true
        };
    }
}