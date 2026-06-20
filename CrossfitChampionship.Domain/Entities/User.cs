using CrossfitChampionship.Domain.Enums;

namespace CrossfitChampionship.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public string Nome { get; private set; } = null!;
    public UserRole Role { get; private set; }

    private User() { }

    public User(string username, string password, string nome, UserRole role)
    {
        Username = username;
        Password = password;
        Nome = nome;
        Role = role;
    }
}
