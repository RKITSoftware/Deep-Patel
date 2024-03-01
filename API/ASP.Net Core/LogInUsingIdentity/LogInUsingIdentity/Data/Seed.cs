using LogInUsingIdentity.Models;
using Microsoft.AspNetCore.Identity;

namespace LogInUsingIdentity.Data
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "dp3676991@gmail.com";

                try
                {
                    var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                    if (adminUser == null)
                    {
                        var newAdminUser = new AppUser()
                        {
                            UserName = "deep",
                            Email = adminUserEmail,
                            EmailConfirmed = true
                        };
                        var result = await userManager.CreateAsync(newAdminUser, "Deep@2513?");
                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                string appUserEmail = "user1@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "1111",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        //Adress = new Address() { Street = "Surat" },
                        //Department = new Department() { Name = "Computer" }
                    };
                    var result = await userManager.CreateAsync(newAppUser, "@User2513");
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
