using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace Core.Modules.UserModule.List
{
    public class ListUserHandler : IRequestHandler<ListUserQuery, List<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ListUserHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(ListUserQuery request, CancellationToken cancellationToken)
        {
            List<UserEntity> userEntities = await _userRepository.GetUsersAsync();
            List<UserDto> usersDto = _mapper.Map<List<UserDto>>(userEntities);
            foreach (UserDto user in usersDto)
            {
                UserEntity userEntity = _mapper.Map<UserEntity>(user);
                UserEntity userData = await _userRepository.GetByEmailAsync(userEntity.Email);
                user.Roles = await _userRepository.GetUserRolesAsync(userData);
            }

            return usersDto;
        }
    }
}
