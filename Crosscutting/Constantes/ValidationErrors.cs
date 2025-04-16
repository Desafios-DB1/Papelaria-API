namespace Crosscutting.Constantes;

public static class ValidationErrors
{
    public static string CampoObrigatorio => "{PropertyName} é obrigatório.";
    public static string CampoInvalido => "{PropertyName} é inválido.";
    public static string TamanhoMaximo(int max) => "{PropertyName} deve ter no máximo " + max + " caracteres.";
    public static string ValorMinimo(int quantidade) => "{PropertyName} deve ser no mínimo" + quantidade + "!";
    public static string ValorNaoNegativo(string campo) => $"{campo} não pode ser menor que zero!";
    public static string QuantidadeInsuficiente => "Quantidade insuficiente em estoque!";
}
