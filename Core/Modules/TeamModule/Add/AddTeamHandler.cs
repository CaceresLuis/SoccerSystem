using MediatR;
using AutoMapper;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Shared.Helpers.Image;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.Add
{
    public class AddTeamHandler : IRequestHandler<AddTeamCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IIMageHelper _iMageHelper;
        private readonly ITeamRepository _teamRepository;

        public AddTeamHandler(ITeamRepository teamRepository, IMapper mapper, IIMageHelper iMageHelper)
        {
            _mapper = mapper;
            _iMageHelper = iMageHelper;
            _teamRepository = teamRepository;
        }

        public async Task<bool> Handle(AddTeamCommand request, CancellationToken cancellationToken)
        {
            if (request.Team.LogoFile != null)
                request.Team.LogoPath = await _iMageHelper.UploadImageAsync(request.Team.LogoFile, "Tournaments");

            TeamEntity team = _mapper.Map<TeamEntity>(request.Team);

            if(await _teamRepository.FindTeamByNameAsync(team.Name) != null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = $"The {team.Name} is already registered",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if (!await _teamRepository.AddTeamAsync(team))
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "Something has gone wrong",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            return true;
        }
    }
}
