using System;
using NUnit.Framework;
using enigma.console;

namespace enigma.tests;

public class MachineTests
{
    [Test]
    public void SimpleEncodeDecode_ReturnsOriginalMessage()
    {
        var rotors = new[] { "BDFHJLCPRTXVZNYEIWGAKMUSQO" };
        var machine = new Machine(3, rotors);

        var original = "HELLO";
        var encoded = machine.Run("encode", original);
        var decoded = machine.Run("decode", encoded);

        Assert.That(decoded, Is.EqualTo(original));
    }

    [TestCase("HELLO")]
    [TestCase("")]
    [TestCase("A")]
    [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    public void EncodeDecode_RoundTrips_ForVariousMessages(string original)
    {
        var rotors = new[] { "BDFHJLCPRTXVZNYEIWGAKMUSQO", "AJDKSIRUXBLHWTMCQGZNPYFVOE" };
        var machine = new Machine(5, rotors);

        var encoded = machine.Run("encode", original);
        var decoded = machine.Run("decode", encoded);

        Assert.That(decoded, Is.EqualTo(original.ToUpperInvariant()));
    }

    [Test]
    public void Encode_WithMultipleRotors_ProducesDifferentThanShiftOnly()
    {
        var rotors = new[] { "BDFHJLCPRTXVZNYEIWGAKMUSQO", "AJDKSIRUXBLHWTMCQGZNPYFVOE" };
        var machine = new Machine(2, rotors);

        var original = "TESTMESSAGE";
        var shiftOnly = Machine.ShiftMessage(original.ToUpperInvariant(), 2, CreateAlphabetCharDict(), CreateAlphabetIndexDict());
        var encoded = machine.Run("encode", original);

        Assert.That(encoded, Is.Not.EqualTo(shiftOnly));
    }

    [Test]
    public void Run_WithLowercaseInput_IsHandledCaseInsensitively()
    {
        var rotors = new[] { "BDFHJLCPRTXVZNYEIWGAKMUSQO" };
        var machine = new Machine(1, rotors);

        var originalLower = "hello";
        var originalUpper = "HELLO";

        var encodedLower = machine.Run("encode", originalLower);
        var encodedUpper = machine.Run("encode", originalUpper);

        Assert.That(encodedLower, Is.EqualTo(encodedUpper));
    }

    [Test]
    public void Run_WithInvalidCharacters_ThrowsArgumentException()
    {
        var rotors = new[] { "BDFHJLCPRTXVZNYEIWGAKMUSQO" };
        var machine = new Machine(1, rotors);

        Assert.Throws<ArgumentException>(() => machine.Run("encode", "HELLO!"));
        Assert.Throws<ArgumentException>(() => machine.Run("decode", "123"));
    }

    private static System.Collections.Generic.Dictionary<char, int> CreateAlphabetCharDict()
    {
        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var dict = new System.Collections.Generic.Dictionary<char, int>();
        for (int i = 0; i < alphabet.Length; i++) dict[alphabet[i]] = i;
        return dict;
    }

    private static System.Collections.Generic.Dictionary<int, char> CreateAlphabetIndexDict()
    {
        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var dict = new System.Collections.Generic.Dictionary<int, char>();
        for (int i = 0; i < alphabet.Length; i++) dict[i] = alphabet[i];
        return dict;
    }
}
