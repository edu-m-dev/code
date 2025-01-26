namespace processBigFile.console;

public record ReadableFile(string FilePath)
{
    public async Task<string[]> ReadAllLinesAsync(CancellationToken token)
    {
        return await File.ReadAllLinesAsync(FilePath, token);
    }

    public string[] ReadAllLines()
    {
        return File.ReadAllLines(FilePath);
    }
}
