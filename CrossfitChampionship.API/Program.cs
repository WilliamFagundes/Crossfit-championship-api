using CrossfitChampionship.Application.Interfaces;
using CrossfitChampionship.Application.Services;
using CrossfitChampionship.Infrastructure;
using CrossfitChampionship.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("SupabaseConnection")
    ?? throw new InvalidOperationException("Connection string 'SupabaseConnection' not found.");
builder.Services.AddInfrastructure(connectionString);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.WebHost.UseUrls("http://localhost:5000");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(context);
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crossfit Championship API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
