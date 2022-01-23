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
            Email = "a@a.a",
            UserName = "BebraAdmin"
        };

        var driver = new IdentityUser()
        {
            Email = "b@b.b",
            UserName = "BebraDriver"
        };

        var user = new IdentityUser()
        {
            Email = "c@c.c",
            UserName = "BebraUser"
        };

        var adminRole = new IdentityRole() { Name = "Admin" };
        var driverRole = new IdentityRole() { Name = "Driver" };
        var userRole = new IdentityRole() { Name = "User" };

        _userManager.CreateAsync(admin, "admin").GetAwaiter().GetResult();
        _userManager.CreateAsync(driver, "driver").GetAwaiter().GetResult();
        _userManager.CreateAsync(user, "user").GetAwaiter().GetResult();

        _roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
        _roleManager.CreateAsync(driverRole).GetAwaiter().GetResult();
        _roleManager.CreateAsync(userRole).GetAwaiter().GetResult();

        _userManager.AddToRoleAsync(admin, adminRole.Name);
        _userManager.AddToRoleAsync(driver, driverRole.Name);
        _userManager.AddToRoleAsync(user, userRole.Name);
    }
}