using System.Net;
using System.Net.Http.Json;
using chores.bl.ef;
using FluentAssertions;
using Xunit;

namespace chores.webapi.tests.integration;

public class ChoresControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ChoresControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task AddChore_ThenGetAllChores_WorksEndToEnd()
    {
        // Arrange: create a new chore
        var newChore = new Chore { Name = "Vacuum", Description = "Vacuum the floor" };

        // Act: POST chore
        var postResponse = await _client.PostAsJsonAsync("/api/chores/addChore", newChore);
        postResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var createdChore = await postResponse.Content.ReadFromJsonAsync<Chore>();
        createdChore.Should().NotBeNull();
        createdChore.Name.Should().Be(newChore.Name);
        createdChore.Description.Should().Be(newChore.Description);

        // Act: GET chores
        var getResponse = await _client.GetAsync("/api/chores/getAllChores");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var chores = await getResponse.Content.ReadFromJsonAsync<List<Chore>>();
        chores.Should().ContainSingle(c => c.Name == newChore.Name && c.Description == newChore.Description);
    }
}
