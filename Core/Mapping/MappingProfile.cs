using AutoMapper;
using Core.ModelResponse;
using Infrastructure.Models;

namespace Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TournamentEntity, TournamentResponse>().ReverseMap();
            //CreateMap<TournamentEntity, TournamentResponse>().ReverseMap();
        }
    }
}
