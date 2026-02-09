using MediatR;

namespace chores.core.getAllChores;

public class GetAllChoresQuery : IRequest<IEnumerable<ChoreDto>>
{
}
