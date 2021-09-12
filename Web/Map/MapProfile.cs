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
            CreateMap<TeamResponse, TeamViewModel>().ReverseMap();

            CreateMap<TournamentEntity, TournamentResponse>().ReverseMap();
            CreateMap<TournamentResponse, TournamentViewModel>().ReverseMap();

            CreateMap<GroupEntity, GroupResponse>().ReverseMap();
            CreateMap<GroupResponse, GroupViewModel>().ReverseMap();




            CreateMap<MatchEntity, MatchResponse>().ReverseMap();
            CreateMap<GroupDetailEntity, AGroupDetailResponse>().ReverseMap();


            CreateMap<TeamResponse, Team>().ReverseMap();
            CreateMap<MatchResponse, Match>().ReverseMap();
            CreateMap<GroupResponse, Group>().ReverseMap();
            CreateMap<TournamentResponse, Tournament>().ReverseMap();
            CreateMap<AGroupDetailResponse, GroupDetails>().ReverseMap();
            CreateMap<GroupDetailsResponse, CreateGroupDetailsViewModel>().ReverseMap();
        }
    }
}
