using Microsoft.AspNetCore.Identity;
using Shopping_Cart_UI.Roles;

namespace Shopping_Cart_UI.Data
{
    public class Dataseed
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userMgr=service.GetService<UserManager<IdentityUser>>();    
            var roleMgr=service.GetService<RoleManager<IdentityRole>>();
            //ADDING ROLES 
            await roleMgr.CreateAsync(new IdentityRole(Roles.Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.Roles.User.ToString()));

            //carete admin user

            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            };

            var isUserExists = await userMgr.FindByEmailAsync(admin.Email);
            if (isUserExists is null)
            {
                await userMgr.CreateAsync(admin,"Admin@123");
                await userMgr.AddToRoleAsync(admin,Roles.Roles.Admin.ToString());
            }
        }
    }
}
