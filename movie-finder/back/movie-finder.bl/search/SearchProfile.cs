using AutoMapper;

namespace movie_finder.bl.search;

public class SearchProfile : Profile
{
    public SearchProfile()
    {
        CreateMap<TMDbLib.Objects.Search.SearchMovie, SearchMovie>();
    }
}
