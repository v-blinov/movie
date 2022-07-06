using MatchActors.Application.Interfaces;
using MatchActors.Infrastructure.MovieClient;
using MatchActors.Infrastructure.Storage;
using MatchActors.Services;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(typeof(Program));

builder.Services.AddScoped<IMovieSearchService, MovieSearchService>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();

var movieClientOptions = builder.Configuration.GetSection(nameof(MovieClientOptions)).Get<MovieClientOptions>();
builder.Services.AddSingleton(movieClientOptions);
builder.Services.AddHttpClient<IMovieClient, MovieClient>(client =>
{
    client.BaseAddress = new Uri(movieClientOptions.BaseUrl);
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();