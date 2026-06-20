using CrossfitChampionship.Domain.Entities;
using CrossfitChampionship.Domain.Enums;

namespace CrossfitChampionship.Infrastructure.Data;

public static class SeedData
{
    private static readonly string[] NomesMista = ["Fênix", "Aurora", "Tempestade", "Horizonte", "Sentinela"];
    private static readonly string[] NomesMasculina = ["Gladiadores", "Titãs", "Cavaleiros", "Leões", "Troianos"];
    private static readonly string[] NomesFeminina = ["Amazonas", "Valquírias", "Sereias", "Fênix", "Corsárias"];
    private static readonly string[] CategoriaNomes = ["RX", "Intermediário", "Scaled"];
    private static readonly Random _rng = new(42);

    public static List<User> GetUsers()
    {
        return
        [
            new User("admin", "admin123", "Administrador", UserRole.Admin)
            {
                Id = 1,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            new User("joao", "123456", "Competitor", UserRole.Competitor)
            {
                Id = 2,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        ];
    }

    public static List<Category> GetCategories()
    {
        return
        [
            new Category("RX") { Id = 1, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category("Intermediário") { Id = 2, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Category("Scaled") { Id = 3, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        ];
    }

    public static List<Workout> GetWorkouts()
    {
        var baseDate = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        return
        [
            new Workout("Fran", "21-15-9 de Thruster (43/35kg) e Pull-ups", WorkoutType.ForTime, baseDate, ScoreType.Time) { Id = 1, CreatedAt = baseDate },
            new Workout("Cindy", "AMRAP 20min: 5 Pull-ups, 10 Flexões, 15 Agachamentos", WorkoutType.AMRAP, baseDate.AddDays(7), ScoreType.Reps) { Id = 2, CreatedAt = baseDate.AddDays(7) },
            new Workout("Deadlift Max", "Deadlift 1RM", WorkoutType.MaxWeight, baseDate.AddDays(14), ScoreType.Weight) { Id = 3, CreatedAt = baseDate.AddDays(14) },
            new Workout("Helen", "3 rounds: 400m corrida, 21 KB swings (24/16kg), 12 Pull-ups", WorkoutType.ForTime, baseDate.AddDays(21), ScoreType.Time) { Id = 4, CreatedAt = baseDate.AddDays(21) }
        ];
    }

    public static List<Team> GetTeams()
    {
        var teams = new List<Team>();
        var id = 1;
        var baseDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        for (var catIdx = 0; catIdx < 3; catIdx++)
        {
            for (var tipoIdx = 0; tipoIdx < 3; tipoIdx++)
            {
                var nomes = tipoIdx switch
                {
                    0 => NomesMista,
                    1 => NomesMasculina,
                    _ => NomesFeminina
                };

                var tipo = (TeamTipo)tipoIdx;
                var categoriaId = catIdx + 1;
                var categoriaNome = CategoriaNomes[catIdx];

                foreach (var nomeBase in nomes)
                {
                    teams.Add(new Team($"{nomeBase} {categoriaNome}", tipo, categoriaId, $"Membro {id}A", $"Membro {id}B")
                    {
                        Id = id++,
                        CreatedAt = baseDate
                    });
                }
            }
        }

        return teams;
    }

    public static List<Score> GetScores()
    {
        var scores = new List<Score>();
        var id = 1;
        var baseDate = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc);

        for (var teamId = 1; teamId <= 45; teamId++)
        {
            for (var workoutId = 1; workoutId <= 4; workoutId++)
            {
                var valor = workoutId switch
                {
                    1 => Math.Round(_rng.NextDouble() * 480 + 120, 1),
                    2 => _rng.Next(50, 301),
                    3 => Math.Round(_rng.NextDouble() * 400 + 100, 1),
                    _ => Math.Round(_rng.NextDouble() * 600 + 300, 1)
                };

                scores.Add(new Score(teamId, workoutId, valor, string.Empty)
                {
                    Id = id++,
                    CreatedAt = baseDate
                });
            }
        }

        return scores;
    }
}
