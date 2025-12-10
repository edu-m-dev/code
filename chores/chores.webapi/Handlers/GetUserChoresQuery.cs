using chores.bl;
using chores.bl.ef;
using MediatR;

public record GetUserChoresQuery(string UserId) : IRequest<IEnumerable<Chore>>;

public class GetUserChoresHandler : IRequestHandler<GetUserChoresQuery, IEnumerable<Chore>>
{
    private readonly IChoresService _service;

    public GetUserChoresHandler(IChoresService service)
    {
        _service = service;
    }

    public Task<IEnumerable<Chore>> Handle(GetUserChoresQuery request, CancellationToken cancellationToken)
    {
        var data = _service.GetUserChores(request.UserId);
        return Task.FromResult(data);
    }
}
