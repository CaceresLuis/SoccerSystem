using Core.Dtos;
using AutoMapper;
using Web.Models;
using Web.ViewModel;
using Web.ModelsView;
using Core.ModelResponse;
using Infrastructure.Models;
using Core.ModelResponse.One;

namespace Web.Map
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<GroupEntity, GroupDto>().ReverseMap();
            CreateMap<GroupEntity, GroupMatchDto>().ReverseMap();
            CreateMap<GroupEntity, GroupMatchsDto>().ReverseMap();
            //CreateMap<GroupEntity, GroupDetailEntity>().ReverseMap();


            CreateMap<MatchEntity, MatchDto>();
            CreateMap<TeamEntity, TeamDto>().ReverseMap();


            CreateMap<AddMatchDto, AddMatchViewModel>();
            CreateMap<GroupDto, GroupViewModel>().ReverseMap();





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
