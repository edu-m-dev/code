namespace Enigma;

public class Machine(int pseudoRandomNumber, string[] rotors)
{
    public string Run(string operation, string message)
    {
        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var indexedAlphabet = alphabet.Select((c, i) => new { c, i });
        var alphabetCharDict = indexedAlphabet.ToDictionary(x => x.c, x => x.i);
        var alphabetIndexDict = indexedAlphabet.ToDictionary(x => x.i, x => x.c);

        if (string.Compare(operation, "encode", StringComparison.InvariantCultureIgnoreCase) == 0)
        {
            message = ShiftMessage(message, pseudoRandomNumber, alphabetCharDict, alphabetIndexDict);
            for (int i = 0; i < rotors.Length; i++)
            {
                message = RotateMessage(message, rotors[i], alphabetCharDict);
            }
        }
        else
        {
            for (int i = rotors.Length - 1; i >= 0; i--)
            {
                var rotor = rotors[i];
                var indexedRotor = rotor.Select((c, i) => new { c, i });
                var rotorCharDict = indexedRotor.ToDictionary(x => x.c, x => x.i);
                message = UnrotateMessage(message, alphabet, rotorCharDict);
            }
            message = UnshiftMessage(message, pseudoRandomNumber, alphabetCharDict, alphabetIndexDict);
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
                var index = (alphabetCharDict[c] + (pseudoRandomNumber + i)) % 26;
                return alphabetIndexDict[index];
            });
        return new string([.. shiftedCharacters]);
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
                var index = (alphabetCharDict[c] - (pseudoRandomNumber + i) + 26 * 10) % 26;
                return alphabetIndexDict[index];
            });
        return new string([.. shiftedCharacters]);
    }
    public static string RotateMessage(
        string message,
        string rotor,
        Dictionary<char, int> alphabetCharDict)
    {
        var rotatedCharacters = message
            .Select(c => rotor[alphabetCharDict[c]]);
        return new string([.. rotatedCharacters]);
    }

    public static string UnrotateMessage(
    string message,
    string alphabet,
    Dictionary<char, int> rotorCharDict)
    {
        var unrotatedCharacters = message
            .Select(c => alphabet[rotorCharDict[c]]);
        return new string([.. unrotatedCharacters]);
    }
}
