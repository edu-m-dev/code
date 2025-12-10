using System.Security.Principal;
using chores.bl;
using chores.bl.ef;
using MediatR;

namespace chores.webapi.Handlers;

public record GetUserChoresQuery(IPrincipal User) : IRequest<IEnumerable<Chore>>;

public class GetUserChoresHandler : IRequestHandler<GetUserChoresQuery, IEnumerable<Chore>>
{
    private readonly IChoresService _service;

    public GetUserChoresHandler(IChoresService service)
    {
        _service = service;
    }

    public Task<IEnumerable<Chore>> Handle(GetUserChoresQuery request, CancellationToken cancellationToken)
    {
        var data = _service.GetChores(request.User);
        return Task.FromResult(data);
    }
}
