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
        var client = new TMDbClient(_appSettings.TmdbApiKey);
        var searchMovies = await client.SearchMovieAsync(searchMovieQuery.Title);
        return
            searchMovies.Results
                .Select(x => _mapper.Map<TMDbLib.Objects.Search.SearchMovie, SearchMovie>(x))
                .ToList();
    }
}
