// See https://aka.ms/new-console-template for more information

using chores.bl.ef;

using (var dbContext = new ChoresDbContext())

    dbContext.Chores.ToList().ForEach(x =>
        Console.WriteLine(x.Name));