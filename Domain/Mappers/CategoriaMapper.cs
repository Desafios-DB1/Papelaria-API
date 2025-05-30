using Crosscutting.Dtos.Categoria;
using Domain.Commands.Categoria;
using Domain.Entities;

namespace Domain.Mappers;

public static class CategoriaMapper
{
    public static Categoria MapToEntity(this CriarCategoriaCommand command)
    {
        return new Categoria
        {
            Nome = command.Nome,
            Descricao = command.Descricao,
            Ativo = true
        };
    }

    public static CategoriaDto MapToDto(this Categoria categoria)
    {
        return new CategoriaDto
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            Ativo = categoria.Ativo
        };
    }
}