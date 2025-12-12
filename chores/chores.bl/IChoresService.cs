using System.Security.Principal;
using chores.bl.ef;

namespace chores.bl;

public interface IChoresService
{
    IEnumerable<Chore> GetChores(IPrincipal user);
    Chore AddChore(IPrincipal user, Chore chore);
}
