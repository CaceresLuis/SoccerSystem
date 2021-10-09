using Core.Dtos;
using AutoMapper;
using Core.Dtos.DtosApi;
using Infrastructure.Models;

namespace Core.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            //TODO
            CreateMap<AddUserDto, UserEntity>();


            CreateMap<TeamEntity, TeamDto>().ReverseMap();
            CreateMap<TeamDto, TeamDtoApi>().ReverseMap();
            CreateMap<UserEntity, UserDto>().ReverseMap();
            CreateMap<UserEntity, UserDtoApi>().ReverseMap();
            CreateMap<MatchEntity, MatchDto>().ReverseMap();
            CreateMap<GroupEntity, GroupDto>().ReverseMap();
            CreateMap<GroupEntity, GroupFullData>().ReverseMap();
            CreateMap<GroupTeamEntity, GroupTeamDto>().ReverseMap();

            CreateMap<GroupEntity, GroupFullDataApi>()
                .ForMember(dto => dto.TournamentId, conf => conf.MapFrom(ent => ent.Id))
                .ReverseMap();

            CreateMap<GroupEntity, GroupMatchsDto>().ReverseMap();
            CreateMap<TournamentEntity, TournamentDto>().ReverseMap();
            CreateMap<TournamentEntity, AddTournamentDto>().ReverseMap();
            CreateMap<TournamentEntity, TournamentFullData>().ReverseMap();
        }
    }
}
