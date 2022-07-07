namespace MatchActors.Contracts;

/// <summary>
/// Модель запроса для получения контента с участием 2 актеров
/// </summary>
public record MatchActorsRequest
{
    /// <summary>
    /// Имя первого актера
    /// </summary>
    public string Actor1 { get; init; } = null!;
    
    /// <summary>
    /// Имя второго актера
    /// </summary>
    public string Actor2 { get; init; } = null!;
    
    /// <summary>
    /// Выбрать ли только фильмы
    /// </summary>
    /// <value>
    ///     true - только фильмы<br/>
    ///     false - любой контент
    /// </value>
    public bool MoviesOnly { get; init; }
}