﻿using Microsoft.AspNetCore.Http;

namespace Core.Dtos.DtosApi
{
    public class AddUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Document { get; set; }
        public string Address { get; set; }
        public IFormFile PicturePath { get; set; }
        public string Token { get; set; }
    }
}
