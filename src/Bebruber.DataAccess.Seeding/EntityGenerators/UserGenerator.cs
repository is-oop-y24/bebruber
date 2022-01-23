using Bebruber.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess.Seeding.EntityGenerators;

public class UserGenerator
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserGenerator(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Seed()
    {
        var admin = new IdentityUser()
        {
            Email = "bebra@bebra.bebra",
            UserName = "Bebra Admin"
        };

        var driver = new IdentityUser()
        {
            Email = "beeeeeeeeeebra@bebebe.bababa",
            UserName = "Bebra Driver"
        };

        var user = new IdentityUser()
        {
            Email = "bebebebebra@bebra.beb",
            UserName = "Bebra User"
        };

        var adminRole = new IdentityRole() { Name = "admin" };
        var driverRole = new IdentityRole() { Name = "driver" };
        var userRole = new IdentityRole() { Name = "user" };

        _userManager.CreateAsync(admin, "admin").GetAwaiter().GetResult();
        _userManager.CreateAsync(driver, "driver").GetAwaiter().GetResult();
        _userManager.CreateAsync(user, "user").GetAwaiter().GetResult();

        _roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
        _roleManager.CreateAsync(driverRole).GetAwaiter().GetResult();
        _roleManager.CreateAsync(userRole).GetAwaiter().GetResult();

        _userManager.AddToRoleAsync(admin, adminRole.Name);
        _userManager.AddToRoleAsync(driver, driverRole.Name);
        _userManager.AddToRoleAsync(driver, driverRole.Name);
    }
}