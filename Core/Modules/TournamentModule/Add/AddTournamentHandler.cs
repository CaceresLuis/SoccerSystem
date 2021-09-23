using System;
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
            Dtos.DtosApi.AddTournamentDto data = request.Tournament;
            if (data.EndDate == DateTime.MinValue)
                data.EndDate = DateTime.Now;

            if (data.StartDate == DateTime.MinValue)
                data.StartDate = DateTime.Now;

            TournamentEntity tournament = _mapper.Map<TournamentEntity>(data);

            if(data.LogoFile != null)
                tournament.LogoPath = await _iMageHelper.UploadImageAsync(data.LogoFile, "Tournaments");

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
