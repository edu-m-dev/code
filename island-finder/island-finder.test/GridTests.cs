using FluentAssertions;
using island_finder.console;

namespace island_finder.test;
public class GridTests
{
    private readonly HashSet<(int islandCount, int[,] grid)> _results = new HashSet<(int, int[,])>
    {
        (5,
        new int[,]
        {
            { 1,0,1,1,0,1},
            { 0,0,1,1,0,0},
            { 1,0,0,0,0,1},
            { 1,1,1,0,0,1},
            { 1,0,1,1,0,1}
        }),
        (1,
        new int[,]
        {
            { 1,1,1,1,1,1},
            { 1,1,1,1,1,1},
            { 1,1,1,1,1,1},
            { 1,1,1,1,1,1},
            { 1,1,1,1,1,1}
        }),
        (0,
        new int[,]
        {
            { 0,0,0,0,0,0},
            { 0,0,0,0,0,0},
            { 0,0,0,0,0,0},
            { 0,0,0,0,0,0},
            { 0,0,0,0,0,0}
        }),
        (1,
        new int[,]
        {
            { 0,1,0,1,0,1},
            { 1,0,1,0,1,0},
            { 0,1,0,1,0,1},
            { 1,0,1,0,1,0},
            { 0,1,0,1,0,1}
        }),
    };


    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestGrids()
    {
        foreach (var x in _results)
        {
            new IslandCounter().CountIslands(x.grid).Should().Be(x.islandCount);
        }
    }
}
