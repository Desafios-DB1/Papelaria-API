using Crosscutting.Dtos.Categoria;

namespace Domain.Entities;

public class Categoria : Entidade
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; } = true;
    
    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    
    public Categoria() {}

    public override void Atualizar<T> (T dto)
    {
        if (dto is CategoriaDto categoriaDto)
        {
            Nome = categoriaDto.Nome;
            Descricao = categoriaDto.Descricao;
            Ativo = categoriaDto.Ativo;
        }
        else
        {
            throw new ArgumentException("Tipo de DTO inválido para atualização.");
        }
    }
}