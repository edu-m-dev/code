using enigma.console;
using Microsoft.AspNetCore.Mvc;

namespace enigma.webapp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnigmaController : ControllerBase
{
    private static readonly string[] _defaultRotors =
    [
        "BDFHJLCPRTXVZNYEIWGAKMUSQO",
        "AJDKSIRUXBLHWTMCQGZNPYFVOE",
        "EKMFLGDQVZNTOWYHXUSPAIBRCJ"
    ];

    public record EnigmaRequest(string Message, int Shift = 1, string[]? Rotors = null);

    [HttpPost("encode")]
    public IActionResult Encode([FromBody] EnigmaRequest req)
    {
        if (req is null || string.IsNullOrEmpty(req.Message))
            return BadRequest("Message is required.");

        var rotors = req.Rotors ?? _defaultRotors;
        var machine = new Machine(req.Shift, rotors);
        var result = machine.Run("encode", req.Message);
        return Ok(new { result });
    }

    [HttpPost("decode")]
    public IActionResult Decode([FromBody] EnigmaRequest req)
    {
        if (req is null || string.IsNullOrEmpty(req.Message))
            return BadRequest("Message is required.");

        var rotors = req.Rotors ?? _defaultRotors;
        var machine = new Machine(req.Shift, rotors);
        var result = machine.Run("decode", req.Message);
        return Ok(new { result });
    }
}
