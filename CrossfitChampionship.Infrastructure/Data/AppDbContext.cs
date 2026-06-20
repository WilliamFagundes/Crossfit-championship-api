using CrossfitChampionship.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrossfitChampionship.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Score> Scores => Set<Score>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nome).IsRequired().HasMaxLength(50);
        });

        builder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
            entity.Property(u => u.Password).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Nome).IsRequired().HasMaxLength(100);
        });

        builder.Entity<Workout>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.Property(w => w.Nome).IsRequired().HasMaxLength(100);
            entity.Property(w => w.Descricao).HasMaxLength(500);
        });

        builder.Entity<Team>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Nome).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Membro1).HasMaxLength(100);
            entity.Property(t => t.Membro2).HasMaxLength(100);

            entity.HasOne(t => t.Categoria)
                  .WithMany()
                  .HasForeignKey(t => t.CategoriaId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Score>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.HasOne(s => s.Team)
                  .WithMany()
                  .HasForeignKey(s => s.TeamId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(s => s.Workout)
                  .WithMany()
                  .HasForeignKey(s => s.WorkoutId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(builder);
    }
}
