using Crosscutting.Dtos.Log;
using Domain.Entities;

namespace Domain.Mappers;

public static class LogProdutoMapper
{
    public static LogDto MapToDto(this LogProduto logProduto)
    {
        return new LogDto
        {
            NomeProduto = logProduto.Produto.Nome,
            NomeUsuarioResponsavel = logProduto.Usuario.NomeUsuario,
            TipoOperacao = logProduto.TipoOperacao,
            QuantidadeAnterior = logProduto.QuantidadeAnterior,
            QuantidadeAtual = logProduto.QuantidadeAtual,
            DataHora = logProduto.DataCriacao
        };
    }
}