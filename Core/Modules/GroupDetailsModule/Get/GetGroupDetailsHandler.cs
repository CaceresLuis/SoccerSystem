using MediatR;
using AutoMapper;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Core.ModelResponse.One;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupDetailsModule.Get
{
    public class GetGroupDetailsHandler : IRequestHandler<GetGroupDetailsQuery, OneGroupDetailsResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGroupDetailsRepository _groupDetailsRepository;

        public GetGroupDetailsHandler(IMapper mapper, IGroupDetailsRepository groupDetailsRepository)
        {
            _mapper = mapper;
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<OneGroupDetailsResponse> Handle(GetGroupDetailsQuery request, CancellationToken cancellationToken)
        {
            OneGroupDetailsResponse response = new OneGroupDetailsResponse { };
            GroupDetailEntity groupDetailEntity = await _groupDetailsRepository.GetGroupDetailsAsync(request.Id);
            if (groupDetailEntity == null)
            {
                response.Data = new ActionResponse { IsSuccess = false, Title = "Error", Message = "The team does not exist", State = State.error };
                return response;
            }

            response.GroupDetail = _mapper.Map<AGroupDetailResponse>(groupDetailEntity);
            return response;
        }
    }
}
