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

namespace Core.Modules.GroupDetailsModule.Get
{
    public class GetGroupDetailsByGroupHandler : IRequestHandler<GetGroupDetailsByGroupQuery, GroupDetailsResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupDetailsRepository _groupDetailsRepository;

        public GetGroupDetailsByGroupHandler(IMapper mapper, IGroupRepository groupRepository, ITeamRepository teamRepository, IGroupDetailsRepository groupDetailsRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _groupRepository = groupRepository;
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<GroupDetailsResponse> Handle(GetGroupDetailsByGroupQuery request, CancellationToken cancellationToken)
        {
            GroupDetailsResponse response = new GroupDetailsResponse { };
            GroupResponse group = _mapper.Map<GroupResponse>(await _groupRepository.FindGroupByIdAsync(request.IdGroup));
            response.Group = group;

            if (group == null)
            {
                response.Data = new ActionResponse { IsSuccess = false, Title = "Error", Message = "The team does not exist", State = State.error };
                return response;
            }

            List<TeamEntity> teams = await _teamRepository.GetAllTeamAsync();
            List<GroupDetailEntity> groupDetails = await _groupDetailsRepository.GetGroupDetailsByGroupAsync(group.Id);

            foreach (GroupDetailEntity groupDetail in groupDetails)
            {
                bool exist = teams.Where(t => t.Id == groupDetail.Team.Id).Any();
                if (exist)
                    teams.Remove(groupDetail.Team);
            }
            List<SelectListItem> teamList = teams.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            response.SelectTeam = teamList;
            return response;
        }
    }
}
