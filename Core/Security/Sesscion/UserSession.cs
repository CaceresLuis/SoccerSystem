using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Core.Security.Sesscion
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetSessionUser()
        {
            return _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
