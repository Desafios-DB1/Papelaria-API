using Crosscutting.Dtos.Produto;
using Domain.Commands.Produto;
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
    public override void Atualizar<T>(T request)
    {
        if (request is AtualizarProdutoCommand atualizarCommand)
        {
            Nome = atualizarCommand.Nome;
            Descricao = atualizarCommand.Descricao;
            Ativo = atualizarCommand.Ativo;
            PrecoCompra = atualizarCommand.PrecoCompra;
            PrecoVenda = atualizarCommand.PrecoVenda;
            CategoriaId = atualizarCommand.CategoriaId;
            QuantidadeEstoque = new QuantidadeEstoque(atualizarCommand.QuantidadeMinima, atualizarCommand.QuantidadeAtual);
        }
        else 
            throw new ArgumentException("Tipo de request inválido para atualização.");
    }
}
