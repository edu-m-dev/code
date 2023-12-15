using AutoMapper;
using MediatR;
using TMDbLib.Client;

namespace movie_finder.bl.search;

public class SearchMovieQueryHandler : IRequestHandler<SearchMovieQuery, IEnumerable<SearchMovie>>
{
    private readonly AppSettings _appSettings;
    private readonly IMapper _mapper;

    public SearchMovieQueryHandler(AppSettings appSettings, IMapper mapper)
    {
        _appSettings = appSettings;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SearchMovie>> Handle(SearchMovieQuery searchMovieQuery, CancellationToken cancellationToken)
    {
        using var client = new TMDbClient(_appSettings.TmdbApiKey); // TODO - if injected as transient, how is the disposing done?
        var searchMovies = await client.SearchMovieAsync(
            query: searchMovieQuery.Title,
            page: default,
            includeAdult: default,
            year: default,
            region: default,
            primaryReleaseYear: default,
            cancellationToken);
        return
            searchMovies.Results
                .Select(x => _mapper.Map<TMDbLib.Objects.Search.SearchMovie, SearchMovie>(x))
                .ToList();
    }
}
