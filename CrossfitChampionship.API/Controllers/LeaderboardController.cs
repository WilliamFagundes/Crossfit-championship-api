using CrossfitChampionship.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitChampionship.API.Controllers;

[ApiController]
[Route("api/leaderboard")]
public class LeaderboardController : ControllerBase
{
    private readonly ILeaderboardService _leaderboardService;

    public LeaderboardController(ILeaderboardService leaderboardService)
    {
        _leaderboardService = leaderboardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLeaderboard([FromQuery] string? tipo, [FromQuery] int? categoriaId)
    {
        var result = await _leaderboardService.GetLeaderboardAsync(tipo, categoriaId);
        return Ok(result);
    }
}
