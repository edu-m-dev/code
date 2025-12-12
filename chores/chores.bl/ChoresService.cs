using System.Security.Principal;
using chores.bl.ef;
using Microsoft.EntityFrameworkCore;

namespace chores.bl;

public class ChoresService : IChoresService
{
    private readonly ChoresDbContext _db;

    public ChoresService(ChoresDbContext db) => _db = db;

    public IEnumerable<Chore> GetChores(IPrincipal user)
    {
        // TODO - For now, chores are global; extend model with UserId if needed
        return _db.Chores.AsNoTracking().ToList();
    }

    public Chore AddChore(IPrincipal user, Chore chore)
    {
        _db.Chores.Add(chore);
        _db.SaveChanges();
        return chore;
    }
}
