using AutoMapper;
using Web.Models;
using Core.ModelResponse;
using Infrastructure.Models;

namespace Web.Map
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<TeamEntity, TeamResponse>().ReverseMap();
            CreateMap<MatchEntity, MatchResponse>().ReverseMap();
            CreateMap<GroupEntity, GroupResponse>().ReverseMap();
            CreateMap<GroupDetailEntity, GroupDetailResponse>().ReverseMap();
            CreateMap<TournamentEntity, TournamentResponse>().ReverseMap();


            CreateMap<TeamResponse, Team>().ReverseMap();
            CreateMap<MatchResponse, Match>().ReverseMap();
            CreateMap<GroupResponse, Group>().ReverseMap();
            CreateMap<GroupDetailResponse, GroupDetails>().ReverseMap();
            CreateMap<TournamentResponse, Tournament>().ReverseMap();
        }
    }
}
