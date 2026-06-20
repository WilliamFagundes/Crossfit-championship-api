namespace CrossfitChampionship.Application.DTOs.Leaderboard;

public class LeaderboardEntryDto
{
    public int Posicao { get; set; }
    public int TeamId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int CategoriaId { get; set; }
    public string Membro1 { get; set; } = string.Empty;
    public string Membro2 { get; set; } = string.Empty;
    public int TotalPontos { get; set; }
    public List<ScoreItemDto> Scores { get; set; } = [];
}
