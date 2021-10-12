using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.List
{
    public class ListTournamentsHandler : IRequestHandler<ListTournamentsQuery, TournamentFullData[]>
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public ListTournamentsHandler(ITournamentRepository tournamentRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<TournamentFullData[]> Handle(ListTournamentsQuery request, CancellationToken cancellationToken)
        {
            TournamentEntity[] tournaments = await _tournamentRepository.GetTournamentsDetailsAsync();
            TournamentFullData[] tournamentdtos = _mapper.Map<TournamentFullData[]>(tournaments);
            foreach (TournamentEntity tour in tournaments)
            {
                ImageEntity img = await _imageRepository.GetImage(tour.Id);
                foreach (TournamentFullData dto in tournamentdtos)
                {
                    if (tour.Id == dto.Id)
                        dto.LogoPath = img.Path;
                }
            }

            return tournamentdtos;
        }
    }
}
