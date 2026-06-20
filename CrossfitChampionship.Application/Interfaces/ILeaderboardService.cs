using CrossfitChampionship.Application.DTOs.Leaderboard;

namespace CrossfitChampionship.Application.Interfaces;

public interface ILeaderboardService
{
    Task<List<LeaderboardEntryDto>> GetLeaderboardAsync(string? tipo, int? categoriaId);
}
