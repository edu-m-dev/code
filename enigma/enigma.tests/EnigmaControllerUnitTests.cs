using System.Reflection;
using enigma.webapp.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace enigma.tests;

public class EnigmaControllerUnitTests
{
    [Test]
    public void Encode_ValidRequest_ReturnsOkWithResult()
    {
        var req = new EnigmaController.EnigmaRequest("HELLO", 1, null);
        var controller = new EnigmaController();

        var actionResult = controller.Encode(req);

        actionResult.Should().BeOfType<OkObjectResult>();
        var ok = (OkObjectResult)actionResult;
        ok.Value.Should().NotBeNull();

        var prop = ok.Value!.GetType().GetProperty("result", BindingFlags.Public | BindingFlags.Instance);
        prop.Should().NotBeNull();
        var resultString = prop!.GetValue(ok.Value) as string;
        resultString.Should().NotBeNullOrEmpty();
    }

    [Test]
    public void Decode_ValidRequest_ReturnsOkWithResult()
    {
        var req = new EnigmaController.EnigmaRequest("HELLO", 2, null);
        var controller = new EnigmaController();

        var actionResult = controller.Decode(req);

        actionResult.Should().BeOfType<OkObjectResult>();
        var ok = (OkObjectResult)actionResult;
        var prop = ok.Value!.GetType().GetProperty("result", BindingFlags.Public | BindingFlags.Instance);
        var resultString = prop!.GetValue(ok.Value) as string;
        resultString.Should().NotBeNullOrEmpty();
    }

    [Test]
    public void Encode_NullRequest_ReturnsBadRequest()
    {
        var controller = new EnigmaController();
        // call with null at runtime; use the null-forgiving operator to satisfy nullable analysis
        var actionResult = controller.Encode(null!);
        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    [Test]
    public void Decode_NullRequest_ReturnsBadRequest()
    {
        var controller = new EnigmaController();
        var actionResult = controller.Decode(null!);
        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    [Test]
    public void Encode_EmptyMessage_ReturnsBadRequest()
    {
        var req = new EnigmaController.EnigmaRequest(string.Empty, 1, null);
        var controller = new EnigmaController();
        var actionResult = controller.Encode(req);
        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    [Test]
    public void Decode_EmptyMessage_ReturnsBadRequest()
    {
        var req = new EnigmaController.EnigmaRequest(string.Empty, 1, null);
        var controller = new EnigmaController();
        var actionResult = controller.Decode(req);
        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    [Test]
    public void Encode_LowercaseMessage_ReturnsOk()
    {
        var req = new EnigmaController.EnigmaRequest("hello", 1, null);
        var controller = new EnigmaController();

        var actionResult = controller.Encode(req);
        actionResult.Should().BeOfType<OkObjectResult>();
    }

    [Test]
    public void Encode_InvalidCharacters_ThrowsArgumentException()
    {
        var req = new EnigmaController.EnigmaRequest("HELLO!", 1, null);
        var controller = new EnigmaController();

        FluentActions.Invoking(() => controller.Encode(req)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Decode_InvalidCharacters_ThrowsArgumentException()
    {
        var req = new EnigmaController.EnigmaRequest("123", 1, null);
        var controller = new EnigmaController();

        FluentActions.Invoking(() => controller.Decode(req)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Encode_WithCustomRotors_ReturnsOk()
    {
        var customRotors = new[] { "EKMFLGDQVZNTOWYHXUSPAIBRCJ" };
        var req = new EnigmaController.EnigmaRequest("TEST", 3, customRotors);
        var controller = new EnigmaController();

        var actionResult = controller.Encode(req);
        actionResult.Should().BeOfType<OkObjectResult>();
    }
}
