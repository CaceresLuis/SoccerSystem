using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.MatchModule.Get
{
    public class GetMatchHandler : IRequestHandler<GetMatchQuery, MatchDto>
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly IMatchRepository _matchRepository;

        public GetMatchHandler(IMapper mapper, IMatchRepository matchRepository, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _matchRepository = matchRepository;
            _imageRepository = imageRepository;
        }

        public async Task<MatchDto> Handle(GetMatchQuery request, CancellationToken cancellationToken)
        {
            MatchEntity match = await _matchRepository.GetMatchAsync(request.Id);
            var imgL = await _imageRepository.GetImage(match.Local.Id);
            var imgV = await _imageRepository.GetImage(match.Visitor.Id);
            var dto = _mapper.Map<MatchDto>(match);

            if (imgL != null && imgV != null)
            {
                dto.Local.LogoPath = imgL.Path;
                dto.Visitor.LogoPath = imgV.Path;
            }

            return dto;
        }
    }
}
