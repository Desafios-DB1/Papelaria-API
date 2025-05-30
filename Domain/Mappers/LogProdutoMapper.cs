﻿using Crosscutting.Dtos.Log;
using Domain.Entities;

namespace Domain.Mappers;

public static class LogProdutoMapper
{
    public static LogDto MapToDto(this LogProduto logProduto)
    {
        return new LogDto
        {
            Id = logProduto.Id,
            NomeProduto = logProduto.Produto.Nome,
            NomeUsuarioResponsavel = logProduto.Usuario.NomeUsuario,
            TipoOperacao = logProduto.TipoOperacao,
            QuantidadeAnterior = logProduto.QuantidadeAnterior,
            NovaQuantidade = logProduto.NovaQuantidade,
            DataHora = logProduto.DataCriacao
        };
    }
}