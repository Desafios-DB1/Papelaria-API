using Crosscutting.Constantes;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings;

public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnName(nameof(Categoria.Id))
            .ValueGeneratedNever();

        builder.Property(c => c.Nome)
            .HasColumnName(nameof(Categoria.Nome))
            .HasMaxLength(Valores.Duzentos);

        builder.Property(c => c.Descricao)
            .HasColumnName(nameof(Categoria.Descricao))
            .HasMaxLength(Valores.Trezentos);

        builder.Property(c => c.Ativo)
            .HasColumnName(nameof(Categoria.Ativo))
            .HasDefaultValue(true);
    }
}