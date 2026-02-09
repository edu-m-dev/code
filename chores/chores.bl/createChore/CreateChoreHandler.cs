using chores.bl.ef;
using MediatR;

namespace chores.core.createChore;

public class CreateChoreHandler : IRequestHandler<CreateChoreRequest>
{
    private readonly ChoresDbContext _choresDbContext;

    public CreateChoreHandler(ChoresDbContext choresDbContext)
    {
        _choresDbContext = choresDbContext;
    }
    public async Task Handle(CreateChoreRequest request, CancellationToken cancellationToken)
    {
        await _choresDbContext.Chores.AddAsync(
            new Chore
            {
                Name = request.Name,
                Description = request.Description
            });
        await _choresDbContext.SaveChangesAsync(cancellationToken);
    }
}
