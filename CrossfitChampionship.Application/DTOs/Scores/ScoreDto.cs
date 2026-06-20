namespace CrossfitChampionship.Application.DTOs.Scores;

public class ScoreDto
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public int WorkoutId { get; set; }
    public double Valor { get; set; }
    public string Observacao { get; set; } = string.Empty;
    public string TeamNome { get; set; } = string.Empty;
    public string WorkoutNome { get; set; } = string.Empty;
}
