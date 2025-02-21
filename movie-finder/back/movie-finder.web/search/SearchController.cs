using MediatR;
using Microsoft.AspNetCore.Mvc;
using movie_finder.bl.search;

namespace movie_finder.web.search;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly ISearchMovieService _movieService;
    private readonly IMediator _mediator;

    public SearchController(
        ILogger<SearchController> logger,
        ISearchMovieService movieService,
        IMediator mediator
        )
    {
        _logger = logger;
        _movieService = movieService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<SearchMovie>> SearchMoviesAsync([FromQuery] SearchMovieQuery searchMovieQuery)
    {
        //return await _movieService.SearchMoviesAsync(searchMovieQuery);
        return await _mediator.Send(searchMovieQuery);
    }
}
