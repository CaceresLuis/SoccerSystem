using MediatR;
using AutoMapper;
using System.Linq;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Exceptions;
using System.Net;

namespace Core.Modules.GroupDetailsModule.Get
{
    public class GetGroupDetailsByGroupHandler : IRequestHandler<GetGroupDetailsByGroupQuery, GroupDetailsResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTeamsRepository _groupDetailsRepository;

        public GetGroupDetailsByGroupHandler(IMapper mapper, IGroupRepository groupRepository, ITeamRepository teamRepository, IGroupTeamsRepository groupDetailsRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _groupRepository = groupRepository;
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<GroupDetailsResponse> Handle(GetGroupDetailsByGroupQuery request, CancellationToken cancellationToken)
        {
            GroupDetailsResponse response = new GroupDetailsResponse { };
            List<GroupEntity> ListGroup = await _groupRepository.GetAllGroupOfTournamentAsync(request.IdTournament);

            GroupEntity groupEntity = await _groupRepository.GetGroupWithTournamentAsync(request.IdGroup);
            GroupResponse group = _mapper.Map<GroupResponse>(groupEntity);
            response.Group = group;

            if (group == null)
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
            foreach (var teambygroup in ListGroup)
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

            response.SelectTeam = teamList;
            return response;
        }
    }
}
