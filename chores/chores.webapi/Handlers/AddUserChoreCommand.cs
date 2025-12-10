using chores.bl;
using chores.bl.ef;
using MediatR;

public record AddUserChoreCommand(string UserId, Chore Chore) : IRequest<Chore>;

public class AddUserChoreHandler : IRequestHandler<AddUserChoreCommand, Chore>
{
    private readonly IChoresService _service;

    public AddUserChoreHandler(IChoresService service)
    {
        _service = service;
    }

    public Task<Chore> Handle(AddUserChoreCommand request, CancellationToken cancellationToken)
    {
        var added = _service.AddUserChore(request.UserId, request.Chore);
        return Task.FromResult(added);
    }
}
