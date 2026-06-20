namespace CrossfitChampionship.Application.DTOs.Users;

public class UserDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
