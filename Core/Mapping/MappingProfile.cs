using AutoMapper;
using Core.ModelResponse;
using Infrastructure.Models;

namespace Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TeamEntity, Team>().ReverseMap();
            CreateMap<GroupEntity, Group>().ReverseMap();
            CreateMap<TournamentEntity, Tournament>().ReverseMap();
        }
    }
}
