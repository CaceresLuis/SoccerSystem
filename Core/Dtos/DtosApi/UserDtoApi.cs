using System.Collections.Generic;

namespace Core.Dtos.DtosApi
{
    public class UserDtoApi
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Document { get; set; }
        public string Address { get; set; }
        public string PicturePath { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}
