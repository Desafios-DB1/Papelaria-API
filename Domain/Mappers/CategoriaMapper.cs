using Crosscutting.Dtos.Categoria;
using Domain.Commands.Categoria;
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

    public static CriarCategoriaCommand MapToCriarCategoriaCommand(this Categoria categoria)
    {
        return new CriarCategoriaCommand
        {
            Nome = categoria.Nome,
            Descricao = categoria.Descricao
        };
    }

    public static Categoria MapToCategoria(this CriarCategoriaCommand command)
    {
        return new Categoria
        {
            Nome = command.Nome,
            Descricao = command.Descricao,
            Ativo = true
        };
    }

    public static CategoriaUpdateRequestDto MapToUpdateDto(this Categoria categoria)
    {
        return new CategoriaUpdateRequestDto
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            Ativo = categoria.Ativo
        };
    }

    public static CategoriaResponseDto MapToResponseDto(this Categoria categoria)
    {
        return new CategoriaResponseDto
        {
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            Ativo = categoria.Ativo,
        };
    }
}