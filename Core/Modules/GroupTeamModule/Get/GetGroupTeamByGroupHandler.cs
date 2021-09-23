using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Net;
using System.Linq;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Modules.GroupTeamModule.Get
{
    public class GetGroupTeamByGroupHandler : IRequestHandler<GetGroupTeamByGroupQuery, AddGroupTeamDto>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTeamsRepository _groupDetailsRepository;

        public GetGroupTeamByGroupHandler(IMapper mapper, IGroupRepository groupRepository, ITeamRepository teamRepository, IGroupTeamsRepository groupDetailsRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _groupRepository = groupRepository;
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<AddGroupTeamDto> Handle(GetGroupTeamByGroupQuery request, CancellationToken cancellationToken)
        {
            List<GroupEntity> listGroup = await _groupRepository.GetAllGroupOfTournamentAsync(request.IdTournament);
            GroupEntity groupEntity = await _groupRepository.GetGroupWithTournamentAsync(request.IdGroup);
            GroupDto groupDto = _mapper.Map<GroupDto>(groupEntity);

            if (groupDto == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The team does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            List<TeamEntity> teams = await _teamRepository.GetAllTeamAsync();

            List<SelectListItem> teamList = new List<SelectListItem>();
            foreach (GroupEntity teambygroup in listGroup)
            {
                List<GroupTeamEntity> groupDetails = await _groupDetailsRepository.GetGroupsDetailsByGroupAsync(teambygroup.Id);
                foreach (GroupTeamEntity groupDetail in groupDetails)
                {
                    bool exist = teams.Where(t => t.Id == groupDetail.Team.Id).Any();
                    if (exist)
                        teams.Remove(groupDetail.Team);
                }
                teamList = teams.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = $"{t.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();
            }

            return new AddGroupTeamDto { Group = groupDto, SelectTeam = teamList };
        }
    }
}
