using CrossfitChampionship.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrossfitChampionship.Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Users.Any())
            return;

        context.Users.AddRange(SeedData.GetUsers());
        context.Categories.AddRange(SeedData.GetCategories());
        context.Workouts.AddRange(SeedData.GetWorkouts());
        context.Teams.AddRange(SeedData.GetTeams());
        context.Scores.AddRange(SeedData.GetScores());
        context.SaveChanges();
    }
}
