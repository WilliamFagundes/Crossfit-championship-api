namespace CrossfitChampionship.Application.DTOs.Scores;

public class CreateScoreDto
{
    public int TeamId { get; set; }
    public int WorkoutId { get; set; }
    public double Valor { get; set; }
    public string? Observacao { get; set; }
}
