using CrossfitChampionship.Application.DTOs.Teams;
using CrossfitChampionship.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrossfitChampionship.API.Controllers;

[ApiController]
[Route("api/teams")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? categoriaId, [FromQuery] string? tipo)
    {
        if (categoriaId.HasValue)
            return Ok(await _teamService.GetByCategoriaAsync(categoriaId.Value));

        if (!string.IsNullOrEmpty(tipo))
            return Ok(await _teamService.GetByTipoAsync(tipo));

        var teams = await _teamService.GetAllAsync();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var team = await _teamService.GetByIdAsync(id);
        if (team is null)
            return NotFound();
        return Ok(team);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeamDto dto)
    {
        var team = await _teamService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = team.Id }, team);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateTeamDto dto)
    {
        try
        {
            var team = await _teamService.UpdateAsync(new UpdateTeamDto
            {
                Id = id,
                Nome = dto.Nome,
                Tipo = dto.Tipo,
                CategoriaId = dto.CategoriaId,
                Membro1 = dto.Membro1,
                Membro2 = dto.Membro2
            });
            return Ok(team);
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
            await _teamService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
