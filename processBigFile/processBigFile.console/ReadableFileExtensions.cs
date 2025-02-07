namespace processBigFile.console;

public static class ReadableFileExtensions
{
    public static async IAsyncEnumerable<string> RemoveBlankLines(this IEnumerable<string> lines, CancellationToken token)
    {
        foreach (var line in lines)
        {
            token.ThrowIfCancellationRequested();
            if (!string.IsNullOrWhiteSpace(line))
            {
                await Task.Delay(1000, token);
                yield return line;
            }
        }
    }
}
