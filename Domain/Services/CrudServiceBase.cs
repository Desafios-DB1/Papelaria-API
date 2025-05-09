using Crosscutting.Constantes;
using Crosscutting.Dtos;
using Crosscutting.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;

namespace Domain.Services;

public abstract class CrudServiceBase<TEntity, TDto>(IRepository<TEntity> repository)
    : ICrudService<TDto>
    where TEntity : Entidade, new()
    where TDto : IBaseDto
{
    public virtual async Task<Guid> CriarAsync(TDto dto)
    {
        if (dto is null)
            throw new RequisicaoInvalidaException(ErrorMessages.ObjetoNulo(typeof(TEntity).Name));
        
        var entity = MapToEntity(dto);
        return await repository.AdicionarESalvarAsync(entity);
    }

    public virtual async Task<TDto> ObterPorIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new RequisicaoInvalidaException(ErrorMessages.CampoNulo("id", typeof(TEntity).Name));
        
        var entity = await repository.ObterPorIdAsync(id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste(typeof(TEntity).Name));
        return MapToResponseDto(entity);
    }
    
    public virtual async Task<List<TDto>> ObterTodosAsync()
    {
        var entities = await repository.ObterTodosAsync();
        return entities.Select(MapToResponseDto).ToList();
    }

    public virtual async Task<Guid> AtualizarAsync(Guid id, TDto dto)
    {
        if (dto is null)
            throw new RequisicaoInvalidaException(ErrorMessages.ObjetoNulo(typeof(TEntity).Name));
        
        var entityExistente = await repository.ObterPorIdAsync(id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste(typeof(TEntity).Name));
        
        entityExistente.Atualizar(dto);
        
        return await repository.AtualizarESalvarAsync(entityExistente);
    }

    public virtual async Task RemoverAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new RequisicaoInvalidaException(ErrorMessages.CampoNulo("id", typeof(TEntity).Name));
        
        var entity = await repository.ObterPorIdAsync(id)
            ?? throw new NaoEncontradoException(ErrorMessages.NaoExiste(typeof(TEntity).Name));
        
        await repository.RemoverESalvarAsync(entity);
    }

    protected abstract TEntity MapToEntity(TDto dto);
    protected abstract TDto MapToResponseDto(TEntity entity);
}