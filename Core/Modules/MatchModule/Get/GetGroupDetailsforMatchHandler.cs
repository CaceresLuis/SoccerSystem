using AutoMapper;
using Core.Dtos;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Modules.MatchModule.Get
{
    public class GetGroupDetailsforMatchHandler : IRequestHandler<GetGroupDetailsforMatchQuery, AddMatchDto>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GetGroupDetailsforMatchHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<AddMatchDto> Handle(GetGroupDetailsforMatchQuery request, CancellationToken cancellationToken)
        {
            GroupEntity groupEntity = await _groupRepository.GetGroupTeamAndDetailsAsync(request.GroupId);
            if (groupEntity == null)
                throw new Exception("error");

            List<SelectListItem> selectTeam = groupEntity.GroupTeams.Select(t => new SelectListItem
            {
                Text = t.Team.Name,
                Value = $"{t.Team.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();
            selectTeam.Insert(0, new SelectListItem
            {
                Text = "[Select a team...]",
                Value = "0"
            });
            GroupDto groupDto = _mapper.Map<GroupDto>(groupEntity);

            return new AddMatchDto { Group = groupDto, Team = selectTeam};
        }
    }
}
