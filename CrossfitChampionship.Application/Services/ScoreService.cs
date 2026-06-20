using CrossfitChampionship.Domain.Entities;
using CrossfitChampionship.Application.DTOs.Scores;
using CrossfitChampionship.Application.Interfaces;

namespace CrossfitChampionship.Application.Services;

public class ScoreService : IScoreService
{
    private readonly IRepository<Score> _scoreRepository;

    public ScoreService(IRepository<Score> scoreRepository)
    {
        _scoreRepository = scoreRepository;
    }

    public async Task<List<ScoreDto>> GetAllAsync()
    {
        var scores = await _scoreRepository.GetAllAsync();
        return scores.Select(s => MapToDto(s)).ToList();
    }

    public async Task<ScoreDto?> GetByIdAsync(int id)
    {
        var score = await _scoreRepository.GetByIdAsync(id);
        return score is null ? null : MapToDto(score);
    }

    public async Task<ScoreDto> CreateAsync(CreateScoreDto dto)
    {
        var entity = new Score(dto.TeamId, dto.WorkoutId, dto.Valor, dto.Observacao ?? string.Empty);
        var created = await _scoreRepository.AddAsync(entity);
        await _scoreRepository.SaveChangesAsync();

        return MapToDto(created);
    }

    public async Task<ScoreDto> UpdateAsync(UpdateScoreDto dto)
    {
        var score = await _scoreRepository.GetByIdAsync(dto.Id);
        if (score is null)
            throw new KeyNotFoundException($"Score with id {dto.Id} not found.");

        score.SetValor(dto.Valor);
        score.SetObservacao(dto.Observacao ?? string.Empty);

        await _scoreRepository.UpdateAsync(score);
        await _scoreRepository.SaveChangesAsync();

        return MapToDto(score);
    }

    public async Task DeleteAsync(int id)
    {
        var score = await _scoreRepository.GetByIdAsync(id);
        if (score is null)
            throw new KeyNotFoundException($"Score with id {id} not found.");

        await _scoreRepository.DeleteAsync(score);
        await _scoreRepository.SaveChangesAsync();
    }

    public async Task<ScoreDto?> GetByTeamAndWorkoutAsync(int teamId, int workoutId)
    {
        var scores = await _scoreRepository.GetAllAsync();
        var score = scores.FirstOrDefault(s => s.TeamId == teamId && s.WorkoutId == workoutId);
        return score is null ? null : MapToDto(score);
    }

    public async Task<List<ScoreDto>> GetByWorkoutAsync(int workoutId)
    {
        var scores = await _scoreRepository.GetAllAsync();
        return scores.Where(s => s.WorkoutId == workoutId).Select(MapToDto).ToList();
    }

    private static ScoreDto MapToDto(Score score)
    {
        return new ScoreDto
        {
            Id = score.Id,
            TeamId = score.TeamId,
            WorkoutId = score.WorkoutId,
            Valor = score.Valor,
            Observacao = score.Observacao ?? string.Empty,
            TeamNome = score.Team?.Nome ?? string.Empty,
            WorkoutNome = score.Workout?.Nome ?? string.Empty
        };
    }
}
