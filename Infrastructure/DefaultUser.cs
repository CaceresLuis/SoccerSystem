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
                string pass = configuration["Password"];
                UserEntity user = new UserEntity
                {
                    FirstName = "Julanito",
                    LastName = "De Tal",
                    UserName = "jusla@nito.com",
                    Document = "2486468794694",
                    Email = "jusla@nito.com"
                };
                string rol = "admin";
                await userManager.CreateAsync(user, pass);
                await roleManager.CreateAsync(new IdentityRole(rol));
                await userManager.AddToRoleAsync(user, rol);
            }
        }
    }
}
