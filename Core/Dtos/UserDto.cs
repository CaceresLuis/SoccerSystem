using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Core.Dtos
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Address { get; set; }
        public string PicturePath { get; set; }
        public IFormFile PictureFile { get; set; }
        public List<string> Roles { get; set; }
    }
}
