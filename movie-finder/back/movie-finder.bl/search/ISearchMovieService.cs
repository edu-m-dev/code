namespace movie_finder.bl.search;

public interface ISearchMovieService
{
    Task<IEnumerable<SearchMovie>> SearchMoviesAsync(SearchMovieQuery searchMovieQuery, CancellationToken ct);
}
