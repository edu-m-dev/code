using AutoMapper;
using wwi.bl.EF;

namespace wwi.Features.People
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, Index.Person>();
        }
    }
}