using SlidingStringMatch.Core;
using Xunit;

namespace SlidingStringMatch.Tests;

public class SlidingStringMatchServiceTests
{
    [Theory]
    [InlineData(null, null, 0)]
    [InlineData(null, "abc", 0)]
    [InlineData("abc", null, 0)]
    public void ReturnsZeroForNullInputs(string s, string p, int expected)
    {
        var actual = SlidingStringMatchService.GetSlidingMatchMaxLength(s, p);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("", "", 0)]
    [InlineData("", "a", 0)]
    [InlineData("a", "", 0)]
    [InlineData("   ", " ", 0)]
    [InlineData(" ", "   ", 0)]
    public void ReturnsZeroForEmptyOrWhitespace(string s, string p, int expected)
    {
        var actual = SlidingStringMatchService.GetSlidingMatchMaxLength(s, p);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("abc", "abc", 3)]
    [InlineData("aaaa", "aaaa", 4)]
    public void ReturnsFullLengthWhenStringsMatch(string s, string p, int expected)
    {
        var actual = SlidingStringMatchService.GetSlidingMatchMaxLength(s, p);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("abc", "xyz", 0)]
    [InlineData("hello", "worms", 0)]
    public void ReturnsZeroWhenNoMatch(string s, string p, int expected)
    {
        var actual = SlidingStringMatchService.GetSlidingMatchMaxLength(s, p);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("abcde", "cdefg", 3)] // 'cde'
    [InlineData("foobar", "barbaz", 3)] // 'bar'
    [InlineData("abab", "babx", 3)] // 'bab'
    [InlineData("bbbbbbbbbbbbbbbbbaaaabbbbbbbbbbbaaaabbbbbbbbbbbbbbbaaabbbbbbbbbbbb", "aaaa", 4)] // 'aaaa'
    public void ReturnsMaxPartialMatchLength(string s, string p, int expected)
    {
        var actual = SlidingStringMatchService.GetSlidingMatchMaxLength(s, p);
        Assert.Equal(expected, actual);
    }
}
