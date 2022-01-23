using Bebruber.DataAccess.Seeding.EntityGenerators;
using Bebruber.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess.Seeding;

public class IdentityDatabaseSeeder
{
    private readonly UserGenerator _userGenerator;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityDatabaseSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;

        _userGenerator = new UserGenerator(userManager, roleManager);
    }

    public void Seed()
    {
        _userGenerator.Seed();
    }
}