using System.Globalization;
using Crosscutting.Constantes;
using Crosscutting.Dtos.Categoria;
using Crosscutting.Exceptions;
using Domain.Interfaces;
using Domain.Mappers;
using Domain.Repositories;

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

    public async Task<CategoriaResponseDto> ObterPorIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new RequisicaoInvalidaException(ErrorMessages.IdNulo("categoria"));

        var categoria = await repository.ObterPorIdAsync(id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste("Categoria"));
        return categoria.MapToResponseDto();
    }

    public async Task<List<CategoriaResponseDto>> ObterPorNome(string nome)
    {
        if (string.IsNullOrEmpty(nome))
            throw new RequisicaoInvalidaException(ErrorMessages.NomeNulo);
        
        var categorias = await repository.ObterPorNomeAsync(nome.ToUpper())
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste("Categoria"));
        
        return categorias.Select(c => c.MapToResponseDto()).ToList();
    }
}