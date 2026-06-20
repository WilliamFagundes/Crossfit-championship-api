using CrossfitChampionship.Application.DTOs.Scores;
using CrossfitChampionship.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitChampionship.API.Controllers;

[ApiController]
[Route("api/scores")]
public class ScoresController : ControllerBase
{
    private readonly IScoreService _scoreService;

    public ScoresController(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    [HttpGet("workout/{workoutId}")]
    public async Task<IActionResult> GetByWorkout(int workoutId)
    {
        var scores = await _scoreService.GetByWorkoutAsync(workoutId);
        return Ok(scores);
    }

    [HttpGet("team/{teamId}/workout/{workoutId}")]
    public async Task<IActionResult> GetByTeamAndWorkout(int teamId, int workoutId)
    {
        var score = await _scoreService.GetByTeamAndWorkoutAsync(teamId, workoutId);
        if (score is null)
            return NotFound();
        return Ok(score);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert([FromBody] CreateScoreDto dto)
    {
        var existing = await _scoreService.GetByTeamAndWorkoutAsync(dto.TeamId, dto.WorkoutId);

        if (existing is not null)
        {
            var updated = await _scoreService.UpdateAsync(new UpdateScoreDto
            {
                Id = existing.Id,
                Valor = dto.Valor,
                Observacao = dto.Observacao
            });
            return Ok(updated);
        }

        var score = await _scoreService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByTeamAndWorkout), new { teamId = dto.TeamId, workoutId = dto.WorkoutId }, score);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateScoreDto dto)
    {
        try
        {
            dto.Id = id;
            var score = await _scoreService.UpdateAsync(dto);
            return Ok(score);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
