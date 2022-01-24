using Bebruber.Application.Handlers.Accounts.Exceptions;
using Bebruber.Application.Requests.Accounts;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Enumerations;
using Bebruber.Domain.ValueObjects.Car;
using Bebruber.Domain.ValueObjects.Card;
using Bebruber.Domain.ValueObjects.User;
using Bebruber.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bebruber.Application.Handlers.Accounts;

public class RegisterDriverHandler : IRequestHandler<RegisterDriver.Command, RegisterDriver.Response>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly BebruberDatabaseContext _databaseContext;
    private readonly IdentityDatabaseContext _identityDatabaseContext;

    public RegisterDriverHandler(
        UserManager<ApplicationUser> userManager,
        BebruberDatabaseContext databaseContext,
        IdentityDatabaseContext identityDatabaseContext)
    {
        _userManager = userManager;
        _databaseContext = databaseContext;
        _identityDatabaseContext = identityDatabaseContext;
    }

    public async Task<RegisterDriver.Response> Handle(
        RegisterDriver.Command request,
        CancellationToken cancellationToken)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

        if (user is not null)
            throw new UserAlreadyExistException(request.Email);

        var newDriver = new Driver(
            new Name(
                request.FirstName,
                request.MiddleName,
                request.LastName),
            new Rating(10),
            new CardInfo(
                new CardNumber(request.CardNumber),
                new ExpirationDate(int.Parse(request.ExpirationDateYear), int.Parse(request.ExpirationDateMonth)), // TODO: fix Parce
                new CvvCode(request.Cvv)),
            new Car(
                new CarBrand(request.CarBrand),
                new CarName(request.CarName),
                CarColor.Parse(request.CarColor),
                CarCategory.Parse(request.CarCategory),
                new CarNumber(
                    new CarNumberRegistrationSeries(request.CarNumber.Substring(0, 6)),
                    new CarNumberRegionCode(request.CarNumber.Substring(6)))),
            new PhoneNumber(request.PhoneNumber));

        await _databaseContext.Drivers.AddAsync(newDriver, cancellationToken);

        IdentityResult? result = await _userManager.CreateAsync(
            new ApplicationUser()
            {
                ModelType = typeof(ApplicationUser),
                ModelId = newDriver.Id,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = $"{newDriver.Name.FirstName}{newDriver.Name.LastName}",
            },
            request.Password);

        if (!result.Succeeded)

            // TODO: CHANGE!!!!
            throw new Exception($"{string.Join(' ', result.Errors.Select(e => e.Description))}");

        await _identityDatabaseContext.SaveChangesAsync(cancellationToken);

        return new RegisterDriver.Response(newDriver.Id);
    }
}