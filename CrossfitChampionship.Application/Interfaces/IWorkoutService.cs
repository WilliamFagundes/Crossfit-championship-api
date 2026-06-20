using CrossfitChampionship.Application.DTOs.Workouts;

namespace CrossfitChampionship.Application.Interfaces;

public interface IWorkoutService
{
    Task<List<WorkoutDto>> GetAllAsync();
    Task<WorkoutDto?> GetByIdAsync(int id);
    Task<WorkoutDto> CreateAsync(CreateWorkoutDto dto);
    Task<WorkoutDto> UpdateAsync(UpdateWorkoutDto dto);
    Task DeleteAsync(int id);
}
