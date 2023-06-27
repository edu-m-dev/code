using AutoMapper;
using TMDbLib.Client;

namespace movie_finder.bl.search;

public class TMDbSearchMovieService : ISearchMovieService
{
    private readonly AppSettings _appSettings;
    private readonly IMapper _mapper;

    public TMDbSearchMovieService(AppSettings appSettings, IMapper mapper)
    {
        _appSettings = appSettings;
        _mapper = mapper;
    }
    public async Task<IEnumerable<SearchMovie>> SearchMoviesAsync(SearchMovieQuery searchMovieQuery)
    {
        var client = new TMDbClient(_appSettings.TmdbApiKey);
        var searchMovies = await client.SearchMovieAsync(searchMovieQuery.Title);
        return
            searchMovies.Results
                .Select(x => _mapper.Map<TMDbLib.Objects.Search.SearchMovie, SearchMovie>(x))
                .ToList();
    }
}
