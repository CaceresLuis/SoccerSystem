using Infrastructure.Models;
using System.Collections.Generic;

namespace Core.Security.Token
{
    public interface IJwtGenerator
    {
        string CreateToken(UserEntity user, List<string> roles);
    }
}