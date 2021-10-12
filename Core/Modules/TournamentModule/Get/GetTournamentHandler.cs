using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.Get
{
    public class GetTournamentHandler : IRequestHandler<GetTournamentQuery, TournamentFullData>
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public GetTournamentHandler(ITournamentRepository tournamentRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<TournamentFullData> Handle(GetTournamentQuery request, CancellationToken cancellationToken)
        {
            TournamentEntity tournament = await _tournamentRepository.GetTournamentDetailsAsync(request.Id);
            ImageEntity img = await _imageRepository.GetImage(tournament.Id);
            if(tournament == null)
                throw new ExceptionHandler(HttpStatusCode.NotFound,
                    new Error
                    {
                        Code = "Error",
                        Message = "The tournament does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });
            TournamentFullData dto = _mapper.Map<TournamentFullData>(tournament);
            dto.LogoPath = img.Path;

            return dto;
        }
    }
}
