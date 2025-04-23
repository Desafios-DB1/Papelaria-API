using Crosscutting.Dtos.Categoria;
using Domain.Entities;

namespace Domain.Mappers;

public static class CategoriaMapper
{
    public static CategoriaCreationRequestDto MapToCreationDto(Categoria categoria)
    {
        return new CategoriaCreationRequestDto
        {
            Nome = categoria.Nome,
            Descricao = categoria.Descricao
        };
    }
}