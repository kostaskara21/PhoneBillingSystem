using Microsoft.AspNetCore.Identity;

namespace TeleProgram.Models
{
    public class SeedingClass
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {

            var rolemanager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "Client", "Seller" };
            foreach (var role in roles)
            {
                if (!await rolemanager.RoleExistsAsync(role))
                {
                    await rolemanager.CreateAsync(new IdentityRole(role));
                }

            }


        }
    }
}
