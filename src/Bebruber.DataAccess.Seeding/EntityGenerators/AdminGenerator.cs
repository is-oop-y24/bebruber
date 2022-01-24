using Bebruber.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess.Seeding.EntityGenerators;

public class AdminGenerator : IEntityGenerator
{
    public AdminGenerator(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var admin = new ApplicationUser
        {
            Email = "admin@bebra.com",
            UserName = "Admin",
        };

        var adminRole = new IdentityRole { Name = "Admin" };

        userManager.CreateAsync(admin, "admin").GetAwaiter().GetResult();
        roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
        userManager.AddToRoleAsync(admin, adminRole.Name).GetAwaiter().GetResult();
    }

    public void Seed(ModelBuilder modelBuilder) { }
}