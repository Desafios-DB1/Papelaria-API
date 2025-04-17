namespace Crosscutting.Constantes;

public static class ValidationErrors
{
    public static string CampoObrigatorio => "{PropertyName} é obrigatório.";
    public static string CampoInvalido => "{PropertyName} é inválido.";
    public static string TamanhoMaximo => "{PropertyName} deve ter no máximo {MaxLenght} caracteres.";
    public static string ValorMinimo => "{PropertyName} deve ser no mínimo {MinValue}.";
    public static string ValorNaoNegativo => "{0} não pode ser menor que zero!";
    public static string QuantidadeInsuficiente => "Quantidade insuficiente em estoque!";
}
