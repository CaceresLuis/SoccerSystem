﻿using MediatR;
using AutoMapper;
using System.Threading;
using Core.ModelResponse;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.List
{
    public class ListTeamsHandler : IRequestHandler<ListTeamsQuery, TeamResponse[]>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;

        public ListTeamsHandler(ITeamRepository teamRepository, IMapper mapper)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<TeamResponse[]> Handle(ListTeamsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<TeamResponse[]>(await _teamRepository.GetAllTeamAsync());
        }
    }
}
