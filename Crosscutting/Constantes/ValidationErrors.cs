namespace Crosscutting.Constantes;

public static class ValidationErrors
{
    public static string CampoObrigatorio => "{PropertyName} é obrigatório.";
    public static string CampoInvalido(string campo) => $"Campo inválido: {campo}";
    public static string TamanhoMaximo => "{PropertyName} deve ter no máximo {MaxLength} caracteres.";
    public static string ValorMinimo => "{PropertyName} deve ser no mínimo {ComparisonValue}.";
    public static string ValorNaoNegativo => "{0} não pode ser menor que zero!";
    public static string QuantidadeInsuficiente => "Quantidade insuficiente em estoque!";
    public static string EmailInvalido => "'{PropertyValue}' não é um email válido.";
    public static string ErrosDeValidacao => "A requisição possui erros de validação";
}
