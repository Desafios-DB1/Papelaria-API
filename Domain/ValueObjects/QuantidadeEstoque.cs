using Crosscutting.Constantes;
using Crosscutting.Exceptions;

namespace Domain.ValueObjects;

public class QuantidadeEstoque
{
    public int QuantidadeMinima { get; private set; }
    public int QuantidadeAtual { get; private set; }

    public QuantidadeEstoque(int quantidadeMinima, int quantidadeAtual)
    {
        if (quantidadeMinima < 0)
            throw new NumeroNaoNegativoException(ValidationErrors.ValorNaoNegativo("Quantidade mínima"));

        if (quantidadeAtual < 0)
            throw new NumeroNaoNegativoException(ValidationErrors.ValorNaoNegativo("Quantidade atual"));

        QuantidadeMinima = quantidadeMinima;
        QuantidadeAtual = quantidadeAtual;
    }

    public bool EstoqueCritico { get; private set; }

    public void RetirarEstoque(int quantidade)
    {
        if (quantidade < 0)
            throw new NumeroNaoNegativoException(ValidationErrors.ValorNaoNegativo("Quantidade a retirar"));
        if (QuantidadeAtual < quantidade)
            throw new QuantidadeInsuficienteException(ValidationErrors.QuantidadeInsuficiente);

        QuantidadeAtual -= quantidade;
        AtualizarStatus();
    }

    public void AdicionarEstoque(int quantidade)
    {
        if (quantidade < 0)
            throw new NumeroNaoNegativoException(ValidationErrors.ValorNaoNegativo("Quantidade a adicionar"));

        QuantidadeAtual += quantidade;
        AtualizarStatus();
    }

    private void AtualizarStatus()
    {
        EstoqueCritico = QuantidadeAtual <= QuantidadeMinima;
    }
}
