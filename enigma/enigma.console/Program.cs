namespace enigma.console;

public static class Program
{
    public static void Main(string[] args)
    {
        var shift = Random.Shared.Next(1, 25);
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
