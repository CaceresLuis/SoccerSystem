using System;
using MediatR;
using Core.Dtos;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Core.Modules.UserModule.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            UserDto userDto = request.UserDto;
            UserEntity user = await _userRepository.GetUserInSesscion();
            if (userDto.Email != user.Email)
                throw new Exception("otro usuario");

            user.Address = userDto.Address ?? user.Address;
            user.Document = userDto.Document ?? user.Document;
            user.LastName = userDto.LastName ?? user.LastName;
            user.FirstName = userDto.FirstName ?? user.FirstName;

            bool update = await _userRepository.UpdateUserAsync(user);
            return update;
        }
    }
}
