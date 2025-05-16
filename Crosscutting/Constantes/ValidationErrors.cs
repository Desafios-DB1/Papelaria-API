namespace Crosscutting.Constantes;

public static class ValidationErrors
{
    public static string CampoObrigatorio => "{PropertyName} é obrigatório.";
    public static string TamanhoMaximo => "{PropertyName} deve ter no máximo {MaxLength} caracteres.";
    public static string ValorMinimo => "{PropertyName} deve ser no mínimo {ComparisonValue}.";
    public static string ValorNaoNegativo => "{0} não pode ser menor que zero.";
    public static string ValorInvalido => "{0} não é um valor válido.";
    public static string QuantidadeInsuficiente => "Quantidade insuficiente no estoque.";
    public static string EmailInvalido => "'{PropertyValue}' não é um email válido.";
    public static string JaExiste(string objeto) => $"Já existe um(a) {objeto} com esse {{PropertyName}}.";
}
