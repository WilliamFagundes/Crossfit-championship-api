using CrossfitChampionship.Application.DTOs.Teams;

namespace CrossfitChampionship.Application.Interfaces;

public interface ITeamService
{
    Task<List<TeamDto>> GetAllAsync();
    Task<TeamDto?> GetByIdAsync(int id);
    Task<TeamDto> CreateAsync(CreateTeamDto dto);
    Task<TeamDto> UpdateAsync(UpdateTeamDto dto);
    Task DeleteAsync(int id);
    Task<List<TeamDto>> GetByCategoriaAsync(int categoriaId);
    Task<List<TeamDto>> GetByTipoAsync(string tipo);
}
