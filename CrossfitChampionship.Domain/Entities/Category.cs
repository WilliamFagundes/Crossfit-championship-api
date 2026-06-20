namespace CrossfitChampionship.Domain.Entities;

public class Category : BaseEntity
{
    public string Nome { get; private set; } = null!;

    private Category() { }

    public Category(string nome)
    {
        Nome = nome;
    }

    public void SetNome(string nome) => Nome = nome;
}
