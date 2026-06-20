namespace CrossfitChampionship.Application.DTOs.Teams;

public class UpdateTeamDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int CategoriaId { get; set; }
    public string Membro1 { get; set; } = string.Empty;
    public string Membro2 { get; set; } = string.Empty;
}
