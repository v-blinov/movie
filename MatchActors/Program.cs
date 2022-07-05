using MatchActors.Infrastructure.Storage;
using MatchActors.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMovieSearchService, MovieSearchService>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();