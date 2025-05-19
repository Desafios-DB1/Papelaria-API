using Crosscutting.Dtos.LogProduto;
using Domain.Entities;

namespace Domain.Mappers;

public static class LogProdutoMapper
{
    public static LogDto MapToDto(this LogProduto logProduto)
    {
        return new LogDto
        {
            ProdutoId = logProduto.ProdutoId,
            NomeProduto = logProduto.Produto?.Nome ?? string.Empty,
            UsuarioId = logProduto.UsuarioId,
            NomeUsuario = logProduto.Usuario?.NomeUsuario ?? string.Empty,
            TipoOperacao = logProduto.TipoOperacao,
            QuantidadeAnterior = logProduto.QuantidadeAnterior,
            QuantidadeAtual = logProduto.QuantidadeAtual,
            DataOperacao = logProduto.DataCriacao
        };
    }
}