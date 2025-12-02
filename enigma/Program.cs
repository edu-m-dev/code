namespace Enigma;

internal static class Program
{
    private const int PseudoRandomNumber = 4;

    private static void Main(string[] args)
    {
        string[] rotors = [
            "BDFHJLCPRTXVZNYEIWGAKMUSQO",
            "AJDKSIRUXBLHWTMCQGZNPYFVOE",
            "EKMFLGDQVZNTOWYHXUSPAIBRCJ"];
        var encryptor = new Machine();
        var encrypted = encryptor.Run("ENCODE", PseudoRandomNumber, rotors, args[0]);
        Console.WriteLine($"encrypted: {encrypted}");
        var decrypted = encryptor.Run("DECODE", PseudoRandomNumber, rotors, encrypted);
        Console.WriteLine($"decrypted: {decrypted}");
        Console.ReadLine();
    }
}
