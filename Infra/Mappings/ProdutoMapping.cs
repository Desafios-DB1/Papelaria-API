using Crosscutting.Constantes;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Id)
            .HasColumnName(nameof(Produto.Id))
            .ValueGeneratedNever();

        builder.Property(p => p.Nome)
            .HasColumnName(nameof(Produto.Nome))
            .HasMaxLength(Valores.Duzentos)
            .IsRequired();

        builder.Property(p => p.Descricao)
            .HasColumnName(nameof(Produto.Descricao))
            .HasMaxLength(Valores.Trezentos);

        builder.Property(p => p.PrecoCompra)
            .HasColumnName(nameof(Produto.PrecoCompra));

        builder.Property(p => p.PrecoVenda)
            .HasColumnName(nameof(Produto.PrecoVenda));
        
        builder.OwnsOne(p => p.QuantidadeEstoque, estoque =>
        {
            estoque.Property(e => e.QuantidadeAtual)
                .HasColumnName(nameof(Produto.QuantidadeEstoque.QuantidadeAtual))
                .IsRequired();

            estoque.Property(e => e.QuantidadeMinima)
                .HasColumnName(nameof(Produto.QuantidadeEstoque.QuantidadeMinima))
                .IsRequired();
        });
        
        builder.HasOne(p => p.Categoria)
            .WithMany(c => c.Produtos)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}