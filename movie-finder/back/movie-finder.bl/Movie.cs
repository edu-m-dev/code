namespace movie_finder.bl;

public class Movie
{
    public int Id { get; set; }
    public string OriginalTitle { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string Homepage { get; set; }
    public string Status { get; set; }
    public string OriginalLanguage { get; set; }
    public long Budget { get; set; }
    public long Revenue { get; set; }
    public double VoteAverage { get; set; }
    public int VoteCount { get; set; }
    public string ImdbId { get; set; }
    public KeywordsContainer Keywords { get; set; }
    public List<Genre> Genres { get; set; }
    public Credits Credits { get; set; }
}
