using CrossfitChampionship.Application.DTOs.Workouts;
using CrossfitChampionship.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitChampionship.API.Controllers;

[ApiController]
[Route("api/workouts")]
public class WorkoutsController : ControllerBase
{
    private readonly IWorkoutService _workoutService;

    public WorkoutsController(IWorkoutService workoutService)
    {
        _workoutService = workoutService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var workouts = await _workoutService.GetAllAsync();
        return Ok(workouts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var workout = await _workoutService.GetByIdAsync(id);
        if (workout is null)
            return NotFound();
        return Ok(workout);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWorkoutDto dto)
    {
        var workout = await _workoutService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = workout.Id }, workout);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateWorkoutDto dto)
    {
        try
        {
            var workout = await _workoutService.UpdateAsync(new UpdateWorkoutDto
            {
                Id = id,
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Tipo = dto.Tipo,
                Data = dto.Data,
                ScoreType = dto.ScoreType
            });
            return Ok(workout);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _workoutService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
