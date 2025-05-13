using Crosscutting.Dtos.Categoria;
using Domain.Entities;

namespace Domain.Mappers;

public static class CategoriaMapper
{
    public static Categoria MapToEntity(this CategoriaDto categoriaDto)
    {
        return new Categoria
        {
            Nome = categoriaDto.Nome,
            Descricao = categoriaDto.Descricao,
            Ativo = true
        };
    }

    public static CategoriaDto MapToDto(this Categoria categoria)
    {
        return new CategoriaDto
        {
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            Ativo = categoria.Ativo
        };
    }
}