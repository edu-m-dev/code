using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace enigma.tests;

public class EnigmaControllerIntegrationTests
{
    private WebApplicationFactory<Program>? _factory;

    private static readonly string[] _testRotors = new[]
    {
        "BDFHJLCPRTXVZNYEIWGAKMUSQO",
        "AJDKSIRUXBLHWTMCQGZNPYFVOE",
        "EKMFLGDQVZNTOWYHXUSPAIBRCJ"
    };

    [SetUp]
    public void SetUp()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [TearDown]
    public void TearDown()
    {
        _factory?.Dispose();
    }

    [Test]
    public async Task PostEncode_Returns200WithResult()
    {
        var client = _factory!.CreateClient();
        var req = new { Message = "HELLO", Shift = 1, Rotors = _testRotors };
        var resp = await client.PostAsJsonAsync("/api/enigma/encode", req);
        resp.EnsureSuccessStatusCode();

        var content = await resp.Content.ReadFromJsonAsync<System.Text.Json.JsonElement>();
        content.GetProperty("result").GetString().Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task PostDecode_Returns200WithResult()
    {
        var client = _factory!.CreateClient();
        var req = new { Message = "HELLO", Shift = 1, Rotors = _testRotors };
        var resp = await client.PostAsJsonAsync("/api/enigma/decode", req);
        resp.EnsureSuccessStatusCode();

        var content = await resp.Content.ReadFromJsonAsync<System.Text.Json.JsonElement>();
        content.GetProperty("result").GetString().Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task PostEncode_InvalidMessage_ReturnsBadRequest()
    {
        var client = _factory!.CreateClient();
        var req = new { Message = "", Shift = 1, Rotors = _testRotors };
        var resp = await client.PostAsJsonAsync("/api/enigma/encode", req);
        resp.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task Encode_Decode()
    {
        var client = _factory!.CreateClient();
        var req = new { Message = "TALESOFTHEUNEXPECTED", Shift = 1, Rotors = _testRotors };
        var encodedResp = await client.PostAsJsonAsync("/api/enigma/encode", req);
        encodedResp.EnsureSuccessStatusCode();

        var encodedContent = await encodedResp.Content.ReadFromJsonAsync<System.Text.Json.JsonElement>();
        var encoded = encodedContent.GetProperty("result").GetString();
        encoded.Should().NotBeNullOrEmpty();

        // Now decode the message
        var decodeResp = await client.PostAsJsonAsync("/api/enigma/decode", new { Message = encoded, Shift = 1, Rotors = _testRotors });
        decodeResp.EnsureSuccessStatusCode();

        var decodeContent = await decodeResp.Content.ReadFromJsonAsync<System.Text.Json.JsonElement>();
        var decoded = decodeContent.GetProperty("result").GetString();
        decoded.Should().NotBeNullOrEmpty();
        decoded.Should().Be(req.Message);
    }
}
