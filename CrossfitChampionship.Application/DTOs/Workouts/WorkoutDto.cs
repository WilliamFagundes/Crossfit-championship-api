namespace CrossfitChampionship.Application.DTOs.Workouts;

public class WorkoutDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public string ScoreType { get; set; } = string.Empty;
}
