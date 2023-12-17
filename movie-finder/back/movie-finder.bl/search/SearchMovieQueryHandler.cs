using AutoMapper;
using MediatR;
using TMDbLib.Client;

namespace movie_finder.bl.search;

public class SearchMovieQueryHandler : IRequestHandler<SearchMovieQuery, IEnumerable<SearchMovie>>
{
    private readonly IMapper _mapper;
    private readonly TMDbClient _tMDbClient;

    public SearchMovieQueryHandler(
        IMapper mapper,
        TMDbClient tMDbClient)
    {
        _mapper = mapper;
        _tMDbClient = tMDbClient;
    }

    public async Task<IEnumerable<SearchMovie>> Handle(SearchMovieQuery searchMovieQuery, CancellationToken cancellationToken)
    {
        var searchMovies = await _tMDbClient.SearchMovieAsync(
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
