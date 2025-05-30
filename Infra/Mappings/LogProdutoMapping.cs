﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings;

public class LogProdutoMapping : IEntityTypeConfiguration<LogProduto>
{
    public void Configure(EntityTypeBuilder<LogProduto> builder)
    {
        builder.ToTable("LogsProduto");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id)
            .HasColumnName(nameof(LogProduto.Id))
            .ValueGeneratedNever();

        builder.HasOne(l => l.Produto)
            .WithMany(p => p.Logs)
            .HasForeignKey(l => l.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(l => l.Usuario)
            .WithMany(u => u.Logs)
            .HasForeignKey(l => l.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(l => l.TipoOperacao)
            .HasColumnName(nameof(LogProduto.TipoOperacao))
            .HasConversion<string>()
            .IsRequired();

        builder.Property(l => l.QuantidadeAnterior)
            .HasColumnName(nameof(LogProduto.QuantidadeAnterior))
            .IsRequired();

        builder.Property(l => l.NovaQuantidade)
            .HasColumnName(nameof(LogProduto.NovaQuantidade))
            .IsRequired();
    }
}