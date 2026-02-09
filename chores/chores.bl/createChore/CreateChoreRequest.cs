using MediatR;

namespace chores.core.createChore;

public class CreateChoreRequest : IRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
}
