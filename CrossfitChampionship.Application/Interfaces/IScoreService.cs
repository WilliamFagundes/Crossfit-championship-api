using CrossfitChampionship.Application.DTOs.Scores;

namespace CrossfitChampionship.Application.Interfaces;

public interface IScoreService
{
    Task<List<ScoreDto>> GetAllAsync();
    Task<ScoreDto?> GetByIdAsync(int id);
    Task<ScoreDto> CreateAsync(CreateScoreDto dto);
    Task<ScoreDto> UpdateAsync(UpdateScoreDto dto);
    Task DeleteAsync(int id);
    Task<ScoreDto?> GetByTeamAndWorkoutAsync(int teamId, int workoutId);
    Task<List<ScoreDto>> GetByWorkoutAsync(int workoutId);
}
