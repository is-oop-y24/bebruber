using Bebruber.Domain.Entities;
using Bebruber.Identity;
using Microsoft.AspNetCore.Identity;

namespace Bebruber.DataAccess.Seeding.EntityGenerators;

public class UserGenerator
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserGenerator(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Seed()
    {
        var admin = new ApplicationUser()
        {
            Email = "a@a.a",
            UserName = "BebraAdmin",
            ModelType = typeof(Client),
        };

        var driver = new ApplicationUser()
        {
            Email = "b@b.b",
            UserName = "BebraDriver",
            ModelType = typeof(Client),
        };

        var user = new ApplicationUser()
        {
            Email = "c@c.c",
            UserName = "BebraUser",
            ModelType = typeof(Client),
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