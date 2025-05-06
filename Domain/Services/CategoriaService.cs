using Crosscutting.Constantes;
using Crosscutting.Dtos.Categoria;
using Crosscutting.Exceptions;
using Domain.Interfaces;
using Domain.Mappers;
using Domain.Repositories;
using FluentValidation;

namespace Domain.Services;

public class CategoriaService(ICategoriaRepository repository, 
    IValidator<CategoriaCreationRequestDto> creationValidator) : ICategoriaService
{
    public async Task<Guid> CriarAsync(CategoriaCreationRequestDto categoriaDto)
    {
        if (categoriaDto is null)
            throw new RequisicaoInvalidaException(ValidationErrors.CampoInvalido("categoria"));

        var validationResult = await creationValidator.ValidateAsync(categoriaDto);
        if (!validationResult.IsValid)
            throw new RequisicaoInvalidaException(validationResult.Errors);

        try
        {
            var categoria = categoriaDto.MapToEntity();
            var newCategoriaId = await repository.AdicionarESalvarAsync(categoria);

            if (newCategoriaId == Guid.Empty)
                throw new ErroNoBancoException(ErrorMessages.BancoFalhou);

            return newCategoriaId;
        }
        catch (ErroNoBancoException ex)
        {
            throw new ErroNoBancoException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new ErroDesconhecidoException(ErrorMessages.ErroDesconhecido, ex);
        }
    }
}