using System.Linq;
using Infrastructure.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class DefaultUser
    {
        public static async Task AddDataUser(DataContext context, UserManager<UserEntity> userManager,RoleManager<IdentityRole> roleManager , IConfiguration configuration)
        {
            if (!userManager.Users.Any())
            {
                //Assing a pass user
                string pass = configuration["Password"];
                UserEntity user = new UserEntity
                {
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = "correo@temp.com",
                    Document = "2486468794694",
                    Email = "correo@temp.com"
                };
                string rol = "admin";
                await userManager.CreateAsync(user, pass);
                await roleManager.CreateAsync(new IdentityRole(rol));
                await userManager.AddToRoleAsync(user, rol);
            }
        }
    }
}
