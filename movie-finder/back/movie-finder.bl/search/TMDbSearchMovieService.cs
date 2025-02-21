using AutoMapper;
using Microsoft.Extensions.Options;
using TMDbLib.Client;

namespace movie_finder.bl.search;

public class TMDbSearchMovieService : ISearchMovieService
{
    private readonly MovieFinderOptions _options;
    private readonly IMapper _mapper;

    public TMDbSearchMovieService(IMapper mapper, IOptions<MovieFinderOptions> options)
    {
        _mapper = mapper;
        _options = options.Value;
    }
    public async Task<IEnumerable<SearchMovie>> SearchMoviesAsync(SearchMovieQuery searchMovieQuery, CancellationToken ct = default)
    {
        var client = new TMDbClient(_options.TmdbApiKey);
        var searchMovies = await client.SearchMovieAsync(
            query: searchMovieQuery.Title,
            page: default,
            includeAdult: default,
            year: default,
            region: default,
            primaryReleaseYear: default,
            ct);
        return
            searchMovies.Results
                .Select(x => _mapper.Map<TMDbLib.Objects.Search.SearchMovie, SearchMovie>(x))
                .ToList();
    }
}
