using SlidingStringMatch.Core;

// calculate the max match length of two sliding strings
var (string1, string2) = ReadTwoStrings();
var maxLength = SlidingStringMatchService.GetSlidingMatchMaxLength(string1, string2);
Console.WriteLine($"Max match length: {maxLength}");

static (string first, string second) ReadTwoStrings()
{
    while (true)
    {
        Console.WriteLine("Please provide exactly two non-empty strings separated by a space, then press Enter.");
        string? input = Console.ReadLine();
        var strings = input?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (strings is { Length: 2 })
        {
            return (strings[0], strings[1]);
        }

        Console.WriteLine("Invalid input.");
    }
}
