using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Modules.TeamModule.Add
{
    public class AddTeamCommand : IRequest<bool>
    {
        public IFormFile LogoFile { get; set; }

        public string Name { get; set; }
    }
}
