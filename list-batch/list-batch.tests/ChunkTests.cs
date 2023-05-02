using list_batch.bl;

namespace list_batch.tests;

[TestClass()]
public class ChunkTests
{
    [TestMethod()]
    public void BuildChunksTests()
    {
        Chunk ch = new Chunk();
        List<int> fullList = new List<int>() { 43, 65, 23, 56, 76, 454, 76, 54, 987 };
        var chunked = ch.BuildChunks(fullList, 3).ToList();

        Assert.AreEqual(3, chunked.Count);
        CollectionAssert.AreEqual(new List<int> { 43, 65, 23 }, chunked[0].ToList());
        CollectionAssert.AreEqual(new List<int> { 56, 76, 454 }, chunked[1].ToList());
        CollectionAssert.AreEqual(new List<int> { 76, 54, 987 }, chunked[2].ToList());

        chunked = ch.BuildChunks(fullList, 2).ToList();
        Assert.AreEqual(5, chunked.Count);
        CollectionAssert.AreEqual(new List<int> { 43, 65 }, chunked[0].ToList());
        CollectionAssert.AreEqual(new List<int> { 23, 56 }, chunked[1].ToList());
        CollectionAssert.AreEqual(new List<int> { 76, 454 }, chunked[2].ToList());
        CollectionAssert.AreEqual(new List<int> { 76, 54 }, chunked[3].ToList());
        CollectionAssert.AreEqual(new List<int> { 987 }, chunked[4].ToList());

        chunked = ch.BuildChunks(fullList, 5).ToList();
        Assert.AreEqual(2, chunked.Count);
        CollectionAssert.AreEqual(new List<int> { 43, 65, 23, 56, 76 }, chunked[0].ToList());
        CollectionAssert.AreEqual(new List<int> { 454, 76, 54, 987 }, chunked[1].ToList());

        chunked = ch.BuildChunks(fullList, 10).ToList();
        Assert.AreEqual(1, chunked.Count);
        CollectionAssert.AreEqual(new List<int> { 43, 65, 23, 56, 76, 454, 76, 54, 987 }, chunked[0].ToList());

        chunked = ch.BuildChunks(fullList, 1).ToList();
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