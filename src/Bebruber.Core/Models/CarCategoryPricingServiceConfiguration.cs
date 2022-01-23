using Bebruber.Core.Exceptions;
using Bebruber.Domain.Enumerations;

namespace Bebruber.Core.Models;

public class CarCategoryPricingServiceConfiguration : ICarCategoryPricingServiceConfiguration
{
    public double CalculatePriceMultiplier(CarCategory category)
    {
        if (category.Equals(CarCategory.Economy))
            return 1.0;
        if (category.Equals(CarCategory.Comfort))
            return 2.0;
        if (category.Equals(CarCategory.Business))
            return 3.0;
        throw new NonExistingCategoryException(category);
    }
}