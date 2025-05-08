using Crosscutting.Constantes;
using Crosscutting.Dtos.Categoria;
using Crosscutting.Exceptions;
using Domain.Commands.Categoria;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Mappers;
using Domain.Repositories;
using FluentValidation;

namespace Domain.Services;

public class CategoriaService(ICategoriaRepository repository) : ICategoriaService
{
    public async Task<Guid> CriarAsync(CriarCategoriaCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new RequisicaoInvalidaException(ErrorMessages.ObjetoNulo("categoria"));
        
        var categoria = request.MapToEntity();
        return await repository.AdicionarESalvarAsync(categoria);
    }

    public async Task<CategoriaResponseDto> ObterPorIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new RequisicaoInvalidaException(ErrorMessages.CampoNulo("id", "categoria"));

        var categoria = await repository.ObterPorIdAsync(id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste("Categoria"));
        return categoria.MapToResponseDto();
    }

    public async Task<CategoriaResponseDto> ObterPorNome(string nome)
    {
        if (string.IsNullOrEmpty(nome))
            throw new RequisicaoInvalidaException(ErrorMessages.CampoNulo("nome"));
        
        var categoria = await repository.ObterPorNomeAsync(nome)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste("Categoria"));

        return categoria.MapToResponseDto();
    }
}