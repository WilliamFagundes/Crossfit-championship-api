using CrossfitChampionship.Domain.Entities;
using CrossfitChampionship.Domain.Enums;
using CrossfitChampionship.Application.DTOs.Workouts;
using CrossfitChampionship.Application.Interfaces;

namespace CrossfitChampionship.Application.Services;

public class WorkoutService : IWorkoutService
{
    private readonly IRepository<Workout> _workoutRepository;

    public WorkoutService(IRepository<Workout> workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public async Task<List<WorkoutDto>> GetAllAsync()
    {
        var workouts = await _workoutRepository.GetAllAsync();
        return workouts.Select(w => new WorkoutDto
        {
            Id = w.Id,
            Nome = w.Nome,
            Descricao = w.Descricao,
            Tipo = w.Tipo.ToString(),
            Data = w.Data,
            ScoreType = w.ScoreType.ToString()
        }).ToList();
    }

    public async Task<WorkoutDto?> GetByIdAsync(int id)
    {
        var workout = await _workoutRepository.GetByIdAsync(id);
        if (workout is null) return null;

        return new WorkoutDto
        {
            Id = workout.Id,
            Nome = workout.Nome,
            Descricao = workout.Descricao,
            Tipo = workout.Tipo.ToString(),
            Data = workout.Data,
            ScoreType = workout.ScoreType.ToString()
        };
    }

    public async Task<WorkoutDto> CreateAsync(CreateWorkoutDto dto)
    {
        var tipo = Enum.Parse<WorkoutType>(dto.Tipo, ignoreCase: true);
        var scoreType = Enum.Parse<ScoreType>(dto.ScoreType, ignoreCase: true);

        var entity = new Workout(dto.Nome, dto.Descricao, tipo, dto.Data, scoreType);
        var created = await _workoutRepository.AddAsync(entity);
        await _workoutRepository.SaveChangesAsync();

        return new WorkoutDto
        {
            Id = created.Id,
            Nome = created.Nome,
            Descricao = created.Descricao,
            Tipo = created.Tipo.ToString(),
            Data = created.Data,
            ScoreType = created.ScoreType.ToString()
        };
    }

    public async Task<WorkoutDto> UpdateAsync(UpdateWorkoutDto dto)
    {
        var workout = await _workoutRepository.GetByIdAsync(dto.Id);
        if (workout is null)
            throw new KeyNotFoundException($"Workout with id {dto.Id} not found.");

        var tipo = Enum.Parse<WorkoutType>(dto.Tipo, ignoreCase: true);
        var scoreType = Enum.Parse<ScoreType>(dto.ScoreType, ignoreCase: true);

        workout.SetNome(dto.Nome);
        workout.SetDescricao(dto.Descricao);
        workout.SetTipo(tipo);
        workout.SetData(dto.Data);
        workout.SetScoreType(scoreType);

        await _workoutRepository.UpdateAsync(workout);
        await _workoutRepository.SaveChangesAsync();

        return new WorkoutDto
        {
            Id = workout.Id,
            Nome = workout.Nome,
            Descricao = workout.Descricao,
            Tipo = workout.Tipo.ToString(),
            Data = workout.Data,
            ScoreType = workout.ScoreType.ToString()
        };
    }

    public async Task DeleteAsync(int id)
    {
        var workout = await _workoutRepository.GetByIdAsync(id);
        if (workout is null)
            throw new KeyNotFoundException($"Workout with id {id} not found.");

        await _workoutRepository.DeleteAsync(workout);
        await _workoutRepository.SaveChangesAsync();
    }
}
