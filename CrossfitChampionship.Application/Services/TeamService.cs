using CrossfitChampionship.Domain.Entities;
using CrossfitChampionship.Domain.Enums;
using CrossfitChampionship.Application.DTOs.Teams;
using CrossfitChampionship.Application.Interfaces;

namespace CrossfitChampionship.Application.Services;

public class TeamService : ITeamService
{
    private readonly IRepository<Team> _teamRepository;

    public TeamService(IRepository<Team> teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<List<TeamDto>> GetAllAsync()
    {
        var teams = await _teamRepository.GetAllAsync();
        return teams.Select(t => MapToDto(t)).ToList();
    }

    public async Task<TeamDto?> GetByIdAsync(int id)
    {
        var team = await _teamRepository.GetByIdAsync(id);
        return team is null ? null : MapToDto(team);
    }

    public async Task<TeamDto> CreateAsync(CreateTeamDto dto)
    {
        var tipo = Enum.Parse<TeamTipo>(dto.Tipo, ignoreCase: true);
        var entity = new Team(dto.Nome, tipo, dto.CategoriaId, dto.Membro1, dto.Membro2);
        var created = await _teamRepository.AddAsync(entity);
        await _teamRepository.SaveChangesAsync();

        return MapToDto(created);
    }

    public async Task<TeamDto> UpdateAsync(UpdateTeamDto dto)
    {
        var team = await _teamRepository.GetByIdAsync(dto.Id);
        if (team is null)
            throw new KeyNotFoundException($"Team with id {dto.Id} not found.");

        var tipo = Enum.Parse<TeamTipo>(dto.Tipo, ignoreCase: true);

        team.SetNome(dto.Nome);
        team.SetTipo(tipo);
        team.SetCategoriaId(dto.CategoriaId);
        team.SetMembro1(dto.Membro1);
        team.SetMembro2(dto.Membro2);

        await _teamRepository.UpdateAsync(team);
        await _teamRepository.SaveChangesAsync();

        return MapToDto(team);
    }

    public async Task DeleteAsync(int id)
    {
        var team = await _teamRepository.GetByIdAsync(id);
        if (team is null)
            throw new KeyNotFoundException($"Team with id {id} not found.");

        await _teamRepository.DeleteAsync(team);
        await _teamRepository.SaveChangesAsync();
    }

    public async Task<List<TeamDto>> GetByCategoriaAsync(int categoriaId)
    {
        var teams = await _teamRepository.GetAllAsync();
        return teams.Where(t => t.CategoriaId == categoriaId).Select(MapToDto).ToList();
    }

    public async Task<List<TeamDto>> GetByTipoAsync(string tipo)
    {
        var teamTipo = Enum.Parse<TeamTipo>(tipo, ignoreCase: true);
        var teams = await _teamRepository.GetAllAsync();
        return teams.Where(t => t.Tipo == teamTipo).Select(MapToDto).ToList();
    }

    private static TeamDto MapToDto(Team team)
    {
        return new TeamDto
        {
            Id = team.Id,
            Nome = team.Nome,
            Tipo = team.Tipo.ToString(),
            CategoriaId = team.CategoriaId,
            CategoriaNome = team.Categoria?.Nome ?? string.Empty,
            Membro1 = team.Membro1,
            Membro2 = team.Membro2
        };
    }
}
