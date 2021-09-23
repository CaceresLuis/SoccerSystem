using MediatR;
using System.Net;
using Shared.Enums;
using Shared.Exceptions;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.UserModule.AddRoleToUer
{
    public class AddRoleToUserHandler : IRequestHandler<AddRoleToUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public AddRoleToUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await _userRepository.GetByEmailAsync(request.Email);
            bool addRolUser = await _userRepository.AddRoleToUser(user, request.RoleName);
            if(!addRolUser)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Not registered",
                        Message = "Rol not added to this user",
                        Title = "No Added",
                        State = State.error,
                        IsSuccess = false
                    });

            return true;
        }
    }
}
