using Crosscutting.Constantes;
using Crosscutting.Exceptions;
using Domain.Enums;

namespace Domain.ValueObjects;

public class QuantidadeEstoque
{
    public int QuantidadeMinima { get; private set; }
    public int QuantidadeAtual { get; private set; }

    public QuantidadeEstoque(int quantidadeMinima, int quantidadeAtual)
    {
        if (quantidadeMinima < 0)
            throw new NumeroNaoNegativoException(string.Format(ValidationErrors.ValorNaoNegativo, "Quantidade mínima"));

        if (quantidadeAtual < 0)
            throw new NumeroNaoNegativoException(string.Format(ValidationErrors.ValorNaoNegativo, "Quantidade atual"));

        QuantidadeMinima = quantidadeMinima;
        QuantidadeAtual = quantidadeAtual;
    }

    public QuantidadeEstoque() { }

    public StatusEstoque StatusEstoque => QuantidadeAtual <= QuantidadeMinima
        ? StatusEstoque.CRITICO
        : StatusEstoque.OK;

    public void RetirarEstoque(int quantidade)
    {
        if (quantidade < 0)
            throw new NumeroNaoNegativoException(string.Format(ValidationErrors.ValorNaoNegativo, "Quantidade a retirar"));
        if (QuantidadeAtual < quantidade)
            throw new QuantidadeInsuficienteException(ValidationErrors.QuantidadeInsuficiente);

        QuantidadeAtual -= quantidade;
    }

    public void AdicionarEstoque(int quantidade)
    {
        if (quantidade < 0)
            throw new NumeroNaoNegativoException(string.Format(ValidationErrors.ValorNaoNegativo, "Quantidade a adicionar"));

        QuantidadeAtual += quantidade;
    }
}
