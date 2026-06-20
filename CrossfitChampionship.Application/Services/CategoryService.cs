using CrossfitChampionship.Domain.Entities;
using CrossfitChampionship.Application.DTOs.Categories;
using CrossfitChampionship.Application.Interfaces;

namespace CrossfitChampionship.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;

    public CategoryService(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Nome = c.Nome
        }).ToList();
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null) return null;

        return new CategoryDto
        {
            Id = category.Id,
            Nome = category.Nome
        };
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var entity = new Category(dto.Nome);
        var created = await _categoryRepository.AddAsync(entity);
        await _categoryRepository.SaveChangesAsync();

        return new CategoryDto
        {
            Id = created.Id,
            Nome = created.Nome
        };
    }

    public async Task<CategoryDto> UpdateAsync(UpdateCategoryDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(dto.Id);
        if (category is null)
            throw new KeyNotFoundException($"Category with id {dto.Id} not found.");

        category.SetNome(dto.Nome);
        await _categoryRepository.UpdateAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return new CategoryDto
        {
            Id = category.Id,
            Nome = category.Nome
        };
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
            throw new KeyNotFoundException($"Category with id {id} not found.");

        await _categoryRepository.DeleteAsync(category);
        await _categoryRepository.SaveChangesAsync();
    }
}
