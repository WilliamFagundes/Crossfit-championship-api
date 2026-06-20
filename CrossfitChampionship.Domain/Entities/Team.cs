using CrossfitChampionship.Domain.Enums;

namespace CrossfitChampionship.Domain.Entities;

public class Team : BaseEntity
{
    public string Nome { get; private set; } = null!;
    public TeamTipo Tipo { get; private set; }
    public int CategoriaId { get; private set; }
    public string Membro1 { get; private set; } = null!;
    public string Membro2 { get; private set; } = null!;
    public Category Categoria { get; private set; } = null!;

    private Team() { }

    public Team(string nome, TeamTipo tipo, int categoriaId, string membro1, string membro2)
    {
        Nome = nome;
        Tipo = tipo;
        CategoriaId = categoriaId;
        Membro1 = membro1;
        Membro2 = membro2;
    }

    public void SetNome(string nome) => Nome = nome;
    public void SetTipo(TeamTipo tipo) => Tipo = tipo;
    public void SetCategoriaId(int categoriaId) => CategoriaId = categoriaId;
    public void SetMembro1(string membro1) => Membro1 = membro1;
    public void SetMembro2(string membro2) => Membro2 = membro2;
}
