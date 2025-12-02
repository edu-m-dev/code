namespace enigma.console;

public class Machine
{
    private readonly int _pseudoRandomNumber;
    private readonly string[] _rotors;

    public Machine(int pseudoRandomNumber, string[] rotors)
    {
        _pseudoRandomNumber = pseudoRandomNumber;
        _rotors = rotors ?? Array.Empty<string>();
    }

    public string Run(string operation, string message)
    {
        if (message is null) throw new ArgumentNullException(nameof(message));
        if (string.IsNullOrEmpty(message)) return message;

        message = message.ToUpperInvariant();

        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var indexedAlphabet = alphabet.Select((c, i) => new { c, i });
        var alphabetCharDict = indexedAlphabet.ToDictionary(x => x.c, x => x.i);
        var alphabetIndexDict = indexedAlphabet.ToDictionary(x => x.i, x => x.c);

        if (string.Equals(operation, "encode", StringComparison.InvariantCultureIgnoreCase))
        {
            message = ShiftMessage(message, _pseudoRandomNumber, alphabetCharDict, alphabetIndexDict);
            for (var i = 0; i < _rotors.Length; i++)
            {
                message = RotateMessage(message, _rotors[i], alphabetCharDict);
            }
        }
        else
        {
            for (var i = _rotors.Length - 1; i >= 0; i--)
            {
                var rotor = _rotors[i];
                var indexedRotor = rotor.Select((c, i) => new { c, i });
                var rotorCharDict = indexedRotor.ToDictionary(x => x.c, x => x.i);
                message = UnrotateMessage(message, alphabet, rotorCharDict);
            }
            message = UnshiftMessage(message, _pseudoRandomNumber, alphabetCharDict, alphabetIndexDict);
        }

        return message;
    }

    public static string ShiftMessage(
        string message,
        int pseudoRandomNumber,
        Dictionary<char, int> alphabetCharDict,
        Dictionary<int, char> alphabetIndexDict)
    {
        var shiftedCharacters = message
            .Select((c, i) =>
            {
                if (!alphabetCharDict.TryGetValue(c, out var charIndex)) throw new ArgumentException($"Invalid character '{c}' in message", nameof(message));
                var index = (charIndex + pseudoRandomNumber + i) % 26;
                return alphabetIndexDict[index];
            });
        return new string(shiftedCharacters.ToArray());
    }

    public static string UnshiftMessage(
        string message,
        int pseudoRandomNumber,
        Dictionary<char, int> alphabetCharDict,
        Dictionary<int, char> alphabetIndexDict)
    {
        var shiftedCharacters = message
            .Select((c, i) =>
            {
                if (!alphabetCharDict.TryGetValue(c, out var charIndex)) throw new ArgumentException($"Invalid character '{c}' in message", nameof(message));
                var index = (charIndex - (pseudoRandomNumber + i) + 26 * 10) % 26;
                return alphabetIndexDict[index];
            });
        return new string(shiftedCharacters.ToArray());
    }

    public static string RotateMessage(
        string message,
        string rotor,
        Dictionary<char, int> alphabetCharDict)
    {
        if (rotor is null) throw new ArgumentNullException(nameof(rotor));
        var rotatedCharacters = message
            .Select(c =>
            {
                if (!alphabetCharDict.TryGetValue(c, out var idx)) throw new ArgumentException($"Invalid character '{c}' in message", nameof(message));
                return rotor[idx];
            });
        return new string(rotatedCharacters.ToArray());
    }

    public static string UnrotateMessage(
        string message,
        string alphabet,
        Dictionary<char, int> rotorCharDict)
    {
        if (alphabet is null) throw new ArgumentNullException(nameof(alphabet));
        var unrotatedCharacters = message
            .Select(c =>
            {
                if (!rotorCharDict.TryGetValue(c, out var idx)) throw new ArgumentException($"Invalid character '{c}' in message", nameof(message));
                return alphabet[idx];
            });
        return new string(unrotatedCharacters.ToArray());
    }
}
