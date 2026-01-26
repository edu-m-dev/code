using System.Security.Principal;
using chores.bl;
using chores.bl.ef;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace chores.tests;

public class ChoresServiceTests
{
    [Fact]
    public void AddChore_Should_Call_SaveChanges()
    {
        // Arrange: use InMemory db and a new context
        var options = new DbContextOptionsBuilder<ChoresDbContext>()
            .UseInMemoryDatabase(databaseName: "code")
            .Options;

        using var context = new ChoresDbContext(options);
        var service = new ChoresService(context);

        var chore = new chores.bl.ef.Chore { Name = "Test", Description = "desc" };
        var principal = new GenericPrincipal(
            new GenericIdentity("test"),
            System.Array.Empty<string>());

        // Act
        service.AddChore(principal, chore);

        // Assert: entry saved to database
        var saved = context.Chores.FirstOrDefault(c => c.Name == "Test");
        Assert.NotNull(saved);
        Assert.Equal("desc", saved.Description);
    }
}
