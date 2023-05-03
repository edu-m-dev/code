using list_batch.bl;

namespace list_batch.tests;

[TestClass()]
public class ChunkTests
{
    [TestMethod()]
    public void BuildChunksWithIterationTest() 
    {
        var func = Chunk.BuildChunksWithIteration<int>;
        BuildChunksTests(func);
    }
    [TestMethod()]
    public void BuildChunksWithRangeTest()
    {
        var func = Chunk.BuildChunksWithRange<int>;
        BuildChunksTests(func);
    }
    [TestMethod()]
    public void BuildChunksWithLinqTest()
    {
        var func = Chunk.BuildChunksWithLinq<int>;
        BuildChunksTests(func);
    }
    [TestMethod()]
    public void BuildChunksWithLinqAndYieldTest()
    {
        var func = Chunk.BuildChunksWithLinqAndYield<int>;
        BuildChunksTests(func);
    }
    private static void BuildChunksTests(Func<List<int>, int, IEnumerable<IEnumerable<int>>> func)
    {        
        List<int> fullList = new() { 43, 65, 23, 56, 76, 454, 76, 54, 987 };
        var chunked = func(fullList, 3).ToList();

        Assert.AreEqual(3, chunked.Count);
        CollectionAssert.AreEqual(new List<int> { 43, 65, 23 }, chunked[0].ToList());
        CollectionAssert.AreEqual(new List<int> { 56, 76, 454 }, chunked[1].ToList());
        CollectionAssert.AreEqual(new List<int> { 76, 54, 987 }, chunked[2].ToList());

        chunked = func(fullList, 2).ToList();
        Assert.AreEqual(5, chunked.Count);
        CollectionAssert.AreEqual(new List<int> { 43, 65 }, chunked[0].ToList());
        CollectionAssert.AreEqual(new List<int> { 23, 56 }, chunked[1].ToList());
        CollectionAssert.AreEqual(new List<int> { 76, 454 }, chunked[2].ToList());
        CollectionAssert.AreEqual(new List<int> { 76, 54 }, chunked[3].ToList());
        CollectionAssert.AreEqual(new List<int> { 987 }, chunked[4].ToList());

        chunked = func(fullList, 5).ToList();
        Assert.AreEqual(2, chunked.Count);
        CollectionAssert.AreEqual(new List<int> { 43, 65, 23, 56, 76 }, chunked[0].ToList());
        CollectionAssert.AreEqual(new List<int> { 454, 76, 54, 987 }, chunked[1].ToList());

        chunked = func(fullList, 10).ToList();
        Assert.AreEqual(1, chunked.Count);
        CollectionAssert.AreEqual(new List<int> { 43, 65, 23, 56, 76, 454, 76, 54, 987 }, chunked[0].ToList());

        chunked = func(fullList, 1).ToList();
        Assert.AreEqual(9, chunked.Count);
        CollectionAssert.AreEqual(new List<int> { 43 }, chunked[0].ToList());
        CollectionAssert.AreEqual(new List<int> { 65 }, chunked[1].ToList());
        CollectionAssert.AreEqual(new List<int> { 23 }, chunked[2].ToList());
        CollectionAssert.AreEqual(new List<int> { 56 }, chunked[3].ToList());
        CollectionAssert.AreEqual(new List<int> { 76 }, chunked[4].ToList());
        CollectionAssert.AreEqual(new List<int> { 454 }, chunked[5].ToList());
        CollectionAssert.AreEqual(new List<int> { 76 }, chunked[6].ToList());
        CollectionAssert.AreEqual(new List<int> { 54 }, chunked[7].ToList());
        CollectionAssert.AreEqual(new List<int> { 987 }, chunked[8].ToList());
    }
}