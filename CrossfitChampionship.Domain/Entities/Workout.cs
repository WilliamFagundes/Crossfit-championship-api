using CrossfitChampionship.Domain.Enums;

namespace CrossfitChampionship.Domain.Entities;

public class Workout : BaseEntity
{
    public string Nome { get; private set; } = null!;
    public string Descricao { get; private set; } = null!;
    public WorkoutType Tipo { get; private set; }
    public DateTime Data { get; private set; }
    public ScoreType ScoreType { get; private set; }

    private Workout() { }

    public Workout(string nome, string descricao, WorkoutType tipo, DateTime data, ScoreType scoreType)
    {
        Nome = nome;
        Descricao = descricao;
        Tipo = tipo;
        Data = data;
        ScoreType = scoreType;
    }

    public void SetNome(string nome) => Nome = nome;
    public void SetDescricao(string descricao) => Descricao = descricao;
    public void SetTipo(WorkoutType tipo) => Tipo = tipo;
    public void SetData(DateTime data) => Data = data;
    public void SetScoreType(ScoreType scoreType) => ScoreType = scoreType;
}
