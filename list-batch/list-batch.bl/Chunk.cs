namespace list_batch.bl;
public class Chunk
{
    public IEnumerable<IEnumerable<T>> BuildChunks<T>(List<T> fullList, int batchSize)
    {
        //return BuildChunksWithIteration(fullList, batchSize);
        return BuildChunksWithRange(fullList, batchSize);
        //return BuildChunksWithLinq(fullList, batchSize);
        //return BuildChunksWithLinqAndYield(fullList, batchSize);
    }

    private IEnumerable<IEnumerable<T>> BuildChunksWithIteration<T>(List<T> fullList, int batchSize)
    {
        var chunkedList = new List<IEnumerable<T>>();
        int count = fullList.Count;
        var position = 0;
        while (position < count)
        {
            var chunk = new List<T>();
            var offset = 0;
            while(offset < batchSize 
                && position + offset + 1 <= count)
            {
                chunk.Add(fullList[position + offset]);
                offset++;
            }
            chunkedList.Add(chunk);
            position += offset;
        }
        return chunkedList;
    }

    private IEnumerable<IEnumerable<T>> BuildChunksWithRange<T>(List<T> fullList, int batchSize)
    {
        var chunkedList = new List<IEnumerable<T>>();
        int count = fullList.Count;
        var position = 0;
        while (position < count)
        {
            if (count - position < batchSize)
            {
                chunkedList.Add(fullList.GetRange(position, count - position));
            }
            else 
            {
                chunkedList.Add(fullList.GetRange(position, batchSize));
            }
            position += batchSize;
        }
        return chunkedList;
    }

    private IEnumerable<IEnumerable<T>> BuildChunksWithLinq<T>(List<T> fullList, int batchSize)
    {
        return fullList.Chunk(batchSize);
    }

    private IEnumerable<IEnumerable<T>> BuildChunksWithLinqAndYield<T>(List<T> fullList, int batchSize)
    {
        int count = fullList.Count;
        var position = 0;
        while (position < count)
        {
            yield return fullList.Skip(position).Take(batchSize);
            position += batchSize;
        }
    }

}