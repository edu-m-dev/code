public class AuthorService
{
    Author[] authors = [
        new Author("steven","hawkins", 10),
        new Author("steve","jobs", 1),
        new Author("rosemund","pilcher", 100)
    ];
    public async IAsyncEnumerable<Author> GetAuthorsAsync()
    {
        foreach (Author author in authors)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            yield return author;
        }
    }
}
