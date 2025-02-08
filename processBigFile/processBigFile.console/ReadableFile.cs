namespace processBigFile.console;

public record ReadableFile(string FilePath)
{
    public async Task<IEnumerable<string>> ReadAllLinesAsync(CancellationToken token)
    {
        return await File.ReadAllLinesAsync(FilePath, token);
    }

    public IEnumerable<string> ReadAllLines()
    {
        return File.ReadAllLines(FilePath);
    }
}
