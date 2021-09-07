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
            CreateMap<MatchEntity, Match>().ReverseMap();
            CreateMap<GroupEntity, Group>().ReverseMap();
            CreateMap<GroupDetailEntity, GroupDetail>().ReverseMap();
            CreateMap<TournamentEntity, Tournament>().ReverseMap();
        }
    }
}
