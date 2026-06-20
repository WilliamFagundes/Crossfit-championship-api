using CrossfitChampionship.Domain.Entities;
using CrossfitChampionship.Domain.Enums;
using CrossfitChampionship.Application.DTOs.Leaderboard;
using CrossfitChampionship.Application.Interfaces;

namespace CrossfitChampionship.Application.Services;

public class LeaderboardService : ILeaderboardService
{
    private readonly IRepository<Team> _teamRepository;
    private readonly IRepository<Workout> _workoutRepository;
    private readonly IRepository<Score> _scoreRepository;

    public LeaderboardService(
        IRepository<Team> teamRepository,
        IRepository<Workout> workoutRepository,
        IRepository<Score> scoreRepository)
    {
        _teamRepository = teamRepository;
        _workoutRepository = workoutRepository;
        _scoreRepository = scoreRepository;
    }

    public async Task<List<LeaderboardEntryDto>> GetLeaderboardAsync(string? tipo, int? categoriaId)
    {
        var teams = await _teamRepository.GetAllAsync();
        var workouts = await _workoutRepository.GetAllAsync();
        var scores = await _scoreRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(tipo))
        {
            var teamTipo = Enum.Parse<TeamTipo>(tipo, ignoreCase: true);
            teams = teams.Where(t => t.Tipo == teamTipo).ToList();
        }

        if (categoriaId.HasValue)
        {
            teams = teams.Where(t => t.CategoriaId == categoriaId.Value).ToList();
        }

        var rankings = new List<LeaderboardEntryDto>();

        foreach (var team in teams)
        {
            var totalPontos = 0;
            var scoreItems = new List<ScoreItemDto>();

            foreach (var workout in workouts)
            {
                var workoutScores = scores
                    .Where(s => s.WorkoutId == workout.Id)
                    .ToList();

                var isWeightType = workout.ScoreType == ScoreType.Weight;
                var orderedScores = isWeightType
                    ? workoutScores.OrderByDescending(s => s.Valor).ToList()
                    : workoutScores.OrderBy(s => s.Valor).ToList();

                var teamScore = scores.FirstOrDefault(s =>
                    s.TeamId == team.Id && s.WorkoutId == workout.Id);

                int pontos;
                if (teamScore is not null)
                {
                    var position = orderedScores.FindIndex(s => s.Id == teamScore.Id) + 1;
                    pontos = position > 0 ? position : orderedScores.Count + 1;
                }
                else
                {
                    pontos = orderedScores.Count + 1;
                }

                totalPontos += pontos;
                scoreItems.Add(new ScoreItemDto
                {
                    WorkoutNome = workout.Nome,
                    Pontos = pontos
                });
            }

            rankings.Add(new LeaderboardEntryDto
            {
                TeamId = team.Id,
                Nome = team.Nome,
                Tipo = team.Tipo.ToString(),
                CategoriaId = team.CategoriaId,
                Membro1 = team.Membro1,
                Membro2 = team.Membro2,
                TotalPontos = totalPontos,
                Scores = scoreItems
            });
        }

        rankings = rankings.OrderBy(r => r.TotalPontos).ToList();

        for (var i = 0; i < rankings.Count; i++)
        {
            rankings[i].Posicao = i + 1;
        }

        return rankings;
    }
}
