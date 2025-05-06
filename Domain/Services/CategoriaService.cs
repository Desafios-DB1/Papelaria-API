using Crosscutting.Dtos.Categoria;
using Domain.Interfaces;
using Domain.Mappers;
using Domain.Repositories;

namespace Domain.Services;

public class CategoriaService(ICategoriaRepository repository) : ICategoriaService
{
    public Task<Guid> CriarAsync(CategoriaCreationRequestDto categoriaDto)
    {
        var categoria = categoriaDto.MapToEntity();
        return repository.AdicionarESalvarAsync(categoria);
    }
}