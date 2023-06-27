namespace movie_finder.bl.search;

public record SearchMovieQuery
{
    public required string Title { get; init; }
};
