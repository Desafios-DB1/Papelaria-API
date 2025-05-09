using Crosscutting.Constantes;
using Crosscutting.Dtos.Categoria;
using Crosscutting.Exceptions;
using Domain.Commands.Categoria;
using Domain.Interfaces;
using Domain.Mappers;
using Domain.Repositories;

namespace Domain.Services;

public class CategoriaService(ICategoriaRepository repository) : ICategoriaService
{
    public async Task<Guid> CriarAsync(CriarCategoriaCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.ObjetoNulo("categoria"));
        
        var categoria = request.MapToCategoria();
        return await repository.AdicionarESalvarAsync(categoria);
    }

    public async Task<CategoriaResponseDto> ObterPorIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new RequisicaoInvalidaException(ErrorMessages.CampoNulo("id", Entidades.Categoria));

        var categoria = await repository.ObterPorIdAsync(id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste(Entidades.Categoria));
        return categoria.MapToResponseDto();
    }

    public async Task<CategoriaResponseDto> ObterPorNomeAsync(string nome)
    {
        if (string.IsNullOrEmpty(nome))
            throw new RequisicaoInvalidaException(ErrorMessages.CampoNulo("nome"));
        
        var categoria = await repository.ObterPorNomeAsync(nome)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste(Entidades.Categoria));

        return categoria.MapToResponseDto();
    }

    public async Task<Guid> AtualizarAsync(CategoriaUpdateRequestDto categoriaDto)
    {
        if (categoriaDto is null)
            throw new RequisicaoInvalidaException(ErrorMessages.ObjetoNulo("categoria"));
        
        var categoriaExistente = await repository.ObterPorIdAsync(categoriaDto.Id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste("Categoria"));
        
        categoriaExistente.Atualizar(categoriaDto);

        return await repository.AtualizarESalvarAsync(categoriaExistente);
    }

    public async Task RemoverAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new RequisicaoInvalidaException(ErrorMessages.CampoNulo("id", Entidades.Categoria));
        
        var categoria = await repository.ObterPorIdAsync(id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste(Entidades.Categoria));
        
        await repository.RemoverESalvarAsync(categoria);
    }
    
    public async Task<List<CategoriaResponseDto>> ObterTodosAsync()
    {
        var categorias = await repository.ObterTodosAsync();
        return categorias.Select(c => c.MapToResponseDto()).ToList();
    }
}