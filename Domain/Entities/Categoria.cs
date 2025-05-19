using Crosscutting.Dtos.Categoria;
using Domain.Commands.Categoria;

namespace Domain.Entities;

public class Categoria : Entidade
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; } = true;
    
    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    
    public Categoria() {}

    public override void Atualizar<T> (T request)
    {
        if (request is AtualizarCategoriaCommand atualizarCommand)
        {
            Nome = atualizarCommand.Nome;
            Descricao = atualizarCommand.Descricao;
            Ativo = atualizarCommand.Ativo;
        }
        else
        {
            throw new ArgumentException("Tipo de request inválido para atualização.");
        }
    }
}