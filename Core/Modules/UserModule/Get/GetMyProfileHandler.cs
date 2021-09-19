using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;

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
            UserEntity user = await _userRepository.GetUserInSesscion();
            List<string> roles = await _userRepository.GetUserRolesAsync(user);
            UserDto userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = roles;
            return userDto;
        }
    }
}
