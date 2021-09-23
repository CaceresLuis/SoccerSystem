using Core.Dtos;
using AutoMapper;
using Core.Dtos.DtosApi;
using Infrastructure.Models;

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
            CreateMap<TournamentEntity, Core.Dtos.TournamentDto>().ReverseMap();
            CreateMap<AddTournamentDto, AddTournamentDto>().ReverseMap();
            CreateMap<TournamentEntity, TournamentFullData>().ReverseMap();
        }
    }
}
