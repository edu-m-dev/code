using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Challenges;

[TestClass()]
public class StonesTests
{
    [TestMethod()]
    public void ReachedLevelIsAsExpected()
    {
        var expected = new[]
        {
            new {
                stones = new[] {1,1,1,1,2,2},
                expectedReachedLevel = 4,
            },
            new {
                stones = new[] {1,1,1,1,1,2,2},
                expectedReachedLevel = 4,
            },
            new {
                stones = new[] { 1,1,1,1,2,2,2,2,3},
                expectedReachedLevel = 5,
            },
            new {
                stones = new[] { 1,1,1,1,2,2,2,2,3,4},
                expectedReachedLevel = 5,
            },
            new {
                stones = new[] { 1,1,1,1,2,2,2,2,3,4,4},
                expectedReachedLevel = 6,
            },
        };

        var tests = expected
            .Select(x =>
                new
                {
                    x.stones,
                    x.expectedReachedLevel,
                    reachedLevel = StonesHelper.GetReachedLevel(x.stones),
                })
            .ToList();

        tests.Select(x => x.expectedReachedLevel)
            .Should().BeEquivalentTo(
                tests.Select(x => x.reachedLevel));
    }
}
