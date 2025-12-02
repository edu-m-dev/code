namespace Enigma;

static class Program
{
    private static void Main(string[] args)
    {
        int shift = Random.Shared.Next(1, 25);
        string[] rotors = [
            "BDFHJLCPRTXVZNYEIWGAKMUSQO",
            "AJDKSIRUXBLHWTMCQGZNPYFVOE",
            "EKMFLGDQVZNTOWYHXUSPAIBRCJ"];
        var encryptor = new Machine(shift, rotors);
        var encrypted = encryptor.Run("ENCODE", args[0]);
        Console.WriteLine($"encrypted: {encrypted}");
        var decrypted = encryptor.Run("DECODE", encrypted);
        Console.WriteLine($"decrypted: {decrypted}");
    }
}
