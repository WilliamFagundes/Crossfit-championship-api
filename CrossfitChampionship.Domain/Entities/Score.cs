namespace CrossfitChampionship.Domain.Entities;

public class Score : BaseEntity
{
    public int TeamId { get; private set; }
    public int WorkoutId { get; private set; }
    public double Valor { get; private set; }
    public string Observacao { get; private set; } = null!;
    public Team Team { get; private set; } = null!;
    public Workout Workout { get; private set; } = null!;

    private Score() { }

    public Score(int teamId, int workoutId, double valor, string observacao)
    {
        TeamId = teamId;
        WorkoutId = workoutId;
        Valor = valor;
        Observacao = observacao;
    }

    public void SetValor(double valor) => Valor = valor;
    public void SetObservacao(string observacao) => Observacao = observacao;
}
