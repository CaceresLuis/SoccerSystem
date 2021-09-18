using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.UserModule.Get
{
    public class GetMyProfileHandler : IRequestHandler<GetMyProfileQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetMyProfileHandler(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            Infrastructure.Models.UserEntity user = await _userRepository.GetUserSesscion();
            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
