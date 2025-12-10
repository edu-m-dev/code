using System.Collections.Generic;
using chores.bl.ef;

namespace chores.bl;

public interface IChoresService
{
    IEnumerable<Chore> GetUserChores(string userId);
    Chore AddUserChore(string userId, Chore chore);
}
