namespace Challenges;

public static class StonesHelper
{
    public static int GetReachedLevel(IReadOnlyList<int> stones)
    {
        var dict = stones
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());

        var promotionsOccurred = false;
        do
        {
            promotionsOccurred = false;
            dict = dict.Select(x =>
                {
                    var pairs = x.Value / 2;
                    var rest = x.Value % 2;
                    var transform = new List<(int level, int count)>();
                    if (pairs > 0)
                    {
                        promotionsOccurred = true;
                        transform.Add((x.Key + 1, pairs));
                    }
                    if (rest == 1)
                    {
                        transform.Add((x.Key, 1));
                    }
                    return transform;
                })
                .SelectMany(x => x)
                .GroupBy(x => x.level)
                .ToDictionary(x => x.Key, x => x.Sum(y => y.count));
        } while (promotionsOccurred);

        return dict.Keys.Max();
    }
}
