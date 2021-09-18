using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.UserModule.Logout
{
    public class LogoutUserHandler : IRequestHandler<LogoutUserQuery>
    {
        private readonly IUserRepository _userRepository;

        public LogoutUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
        {
            await _userRepository.LogoutAsync();
            return Unit.Value;
        }
    }
}
