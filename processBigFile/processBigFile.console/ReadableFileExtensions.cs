using System.Runtime.CompilerServices;

namespace processBigFile.console;

public static class ReadableFileExtensions
{
    public static async IAsyncEnumerable<string> GetOneNonEmptyLinesStream(
        this IEnumerable<string> lines,
        int everyMilliseconds,
        [EnumeratorCancellation] CancellationToken token)
    {
        foreach (var line in lines)
        {
            token.ThrowIfCancellationRequested();
            if (!string.IsNullOrWhiteSpace(line))
            {
                await Task.Delay(everyMilliseconds, token);
                yield return line;
            }
        }
    }
}
