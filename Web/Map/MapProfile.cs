using AutoMapper;
using Web.Models;
using Web.ViewModel;
using Core.ModelResponse;
using Infrastructure.Models;
using Core.ModelResponse.One;

namespace Web.Map
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<TeamEntity, TeamResponse>().ReverseMap();
            CreateMap<MatchEntity, MatchResponse>().ReverseMap();
            CreateMap<GroupEntity, GroupResponse>().ReverseMap();
            CreateMap<GroupDetailEntity, AGroupDetailResponse>().ReverseMap();
            CreateMap<TournamentEntity, TournamentResponse>().ReverseMap();


            CreateMap<TeamResponse, Team>().ReverseMap();
            CreateMap<MatchResponse, Match>().ReverseMap();
            CreateMap<GroupResponse, Group>().ReverseMap();
            CreateMap<TournamentResponse, Tournament>().ReverseMap();
            CreateMap<AGroupDetailResponse, GroupDetails>().ReverseMap();
            CreateMap<GroupDetailsResponse, CreateGroupDetailsViewModel>().ReverseMap();
        }
    }
}
