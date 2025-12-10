using System.Collections.Generic;
using System.Linq;
using chores.bl.ef;
using Microsoft.EntityFrameworkCore;

namespace chores.bl;

public class ChoresService : IChoresService
{
    private readonly ChoresDbContext _db;

    public ChoresService(ChoresDbContext db)
    {
        _db = db;
    }

    public IEnumerable<Chore> GetUserChores(string userId)
    {
        // For now, chores are global; extend model with UserId if needed
        return _db.Chores.AsNoTracking().ToList();
    }

    public Chore AddUserChore(string userId, Chore chore)
    {
        _db.Chores.Add(chore);
        _db.SaveChanges();
        return chore;
    }
}
