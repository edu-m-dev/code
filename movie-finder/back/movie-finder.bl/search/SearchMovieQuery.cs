using MediatR;

namespace movie_finder.bl.search;

public record SearchMovieQuery : IRequest<IEnumerable<SearchMovie>>
{
    public required string Title { get; init; }
};
