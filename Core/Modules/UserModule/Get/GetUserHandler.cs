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
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            UserEntity userEntity = await _userRepository.GetByEmailAsync(request.Email);
            List<string> roles = await _userRepository.GetUserRolesAsync(userEntity);
            UserDto userDto = _mapper.Map<UserDto>(userEntity);
            userDto.Roles = roles;
            return userDto;
        }
    }
}
