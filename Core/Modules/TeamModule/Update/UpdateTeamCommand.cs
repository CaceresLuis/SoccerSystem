using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Modules.TeamModule.Update
{
    public class UpdateTeamCommand : IRequest<bool>
    {
        public IFormFile LogoFile { get; set; }

        public string Name { get; set; }
    }
}
