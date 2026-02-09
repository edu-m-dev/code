using chores.bl.ef;
using chores.core.getAllChores;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace chores.webapi.Handlers;

public class GetAllChoresHandler : IRequestHandler<GetAllChoresQuery, IEnumerable<ChoreDto>>
{
    private readonly ChoresDbContext _choresDbContext;

    public GetAllChoresHandler(ChoresDbContext choresDbContext)
    {
        _choresDbContext = choresDbContext;
    }

    public async Task<IEnumerable<ChoreDto>> Handle(GetAllChoresQuery query, CancellationToken cancellationToken)
    {
        return await _choresDbContext.Chores.AsNoTracking()
            .Select(c => new ChoreDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            })
            .ToListAsync(cancellationToken);
    }
}
