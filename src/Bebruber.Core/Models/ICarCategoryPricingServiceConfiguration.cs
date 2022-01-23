using Bebruber.Domain.Enumerations;

namespace Bebruber.Core.Models;

public interface ICarCategoryPricingServiceConfiguration
{
    double CalculatePriceMultiplier(CarCategory category);
}