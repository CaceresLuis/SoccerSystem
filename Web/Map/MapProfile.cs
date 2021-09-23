using Core.Dtos;
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
            CreateMap<TeamEntity, TeamDto>().ReverseMap();
            CreateMap<UserEntity, UserDto>().ReverseMap();
            CreateMap<MatchEntity, MatchDto>().ReverseMap();
            CreateMap<GroupEntity, GroupDto>().ReverseMap();
            CreateMap<GroupEntity, GroupFullData>().ReverseMap();
            CreateMap<GroupTeamEntity, GroupTeamDto>().ReverseMap();

            CreateMap<GroupEntity, GroupMatchDto>().ReverseMap();
            CreateMap<GroupEntity, GroupMatchsDto>().ReverseMap();
            CreateMap<TournamentEntity, TournamentDto>().ReverseMap();
            CreateMap<GroupTeamEntity, AGroupDetailResponse>().ReverseMap();






            CreateMap<TeamEntity, TeamResponse>().ReverseMap();
            CreateMap<TeamResponse, TeamViewModels>().ReverseMap();

            CreateMap<TournamentEntity, TournamentResponse>().ReverseMap();
            CreateMap<TournamentResponse, TournamentViewModels>().ReverseMap();

            CreateMap<GroupEntity, GroupResponse>().ReverseMap();
            CreateMap<GroupResponse, GroupViewModels>().ReverseMap();




            CreateMap<MatchEntity, MatchResponse>().ReverseMap();
            CreateMap<GroupTeamEntity, AGroupDetailResponse>().ReverseMap();


            CreateMap<TeamResponse, Team>().ReverseMap();
            CreateMap<MatchResponse, Match>().ReverseMap();
            CreateMap<GroupResponse, Group>().ReverseMap();
            CreateMap<TournamentResponse, Tournament>().ReverseMap();
            CreateMap<AGroupDetailResponse, GroupDetails>().ReverseMap();
            CreateMap<GroupDetailsResponse, CreateGroupDetailsViewModels>().ReverseMap();
        }
    }
}
