using MediatR;

namespace movie_finder.bl.search;

public class SearchMovieQueryHandler : IRequestHandler<SearchMovieQuery, IEnumerable<SearchMovie>>
{
    private readonly ISearchMovieService _searchMovieService;
    public SearchMovieQueryHandler(ISearchMovieService searchMovieService)
    {
        _searchMovieService = searchMovieService;
    }

    public async Task<IEnumerable<SearchMovie>> Handle(SearchMovieQuery searchMovieQuery, CancellationToken ct)
    {
        return await _searchMovieService.SearchMoviesAsync(searchMovieQuery, ct);
    }
}
