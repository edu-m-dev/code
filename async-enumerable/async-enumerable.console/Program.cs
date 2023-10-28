await foreach (var author in new AuthorService().GetAuthorsAsync())
{
    Console.WriteLine(author.ToString());
}
Console.ReadLine();
