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

namespace Core.Modules.TournamentModule.Add
{
    public class AddTournamentHandler : IRequestHandler<AddTournamentCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IIMageHelper _iMageHelper;
        private readonly ITournamentRepository _tournamentRepository;

        public AddTournamentHandler(ITournamentRepository tournamentRepository, IMapper mapper, IIMageHelper iMageHelper)
        {
            _mapper = mapper;
            _iMageHelper = iMageHelper;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<bool> Handle(AddTournamentCommand request, CancellationToken cancellationToken)
        {
            if(request.Tournament.LogoFile != null)
                request.Tournament.LogoPath = await _iMageHelper.UploadImageAsync(request.Tournament.LogoFile, "Tournaments");

            TournamentEntity tournament = _mapper.Map<TournamentEntity>(request.Tournament);

            if (await _tournamentRepository.GetTournamentByNameAsync(tournament.Name) != null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = $"The {tournament.Name} tournament name is already registered",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if (!await _tournamentRepository.AddTournamentAsync(tournament))
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
