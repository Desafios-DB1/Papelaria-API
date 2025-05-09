using Crosscutting.Dtos.Produto;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Produto : Entidade
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public QuantidadeEstoque QuantidadeEstoque {get; set; }
    public decimal PrecoCompra { get; set; }
    public decimal PrecoVenda { get; set; }
    public bool Ativo { get; set; }
    
    public Guid CategoriaId { get; set; }
    
    public Categoria Categoria { get; set; }
    public ICollection<LogProduto> Logs { get; set; } = new List<LogProduto>();
    
    public Produto() {}
    public override void Atualizar<T>(T dto)
    {
        if (dto is ProdutoDto produtoDto)
        {
            Nome = produtoDto.Nome;
            Descricao = produtoDto.Descricao;
            Ativo = produtoDto.Ativo;
            PrecoCompra = produtoDto.PrecoCompra;
            PrecoVenda = produtoDto.PrecoVenda;
            CategoriaId = produtoDto.CategoriaId;
            QuantidadeEstoque = new QuantidadeEstoque(produtoDto.QuantidadeAtual, produtoDto.QuantidadeMinima);
        }
        else 
            throw new ArgumentException("Tipo de DTO inválido para atualização.");
    }
}
