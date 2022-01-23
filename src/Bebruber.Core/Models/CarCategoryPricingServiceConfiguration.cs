using Bebruber.Core.Exceptions;
using Bebruber.Domain.Enumerations;

namespace Bebruber.Core.Models;

public class CarCategoryPricingServiceConfiguration : ICarCategoryPricingServiceConfiguration
{
    public double CalculatePriceMultiplier(CarCategory category)
    {
        if (category.Value.Equals(CarCategory.Economy.Value))
            return 1.0;
        if (category.Value.Equals(CarCategory.Comfort.Value))
            return 2.0;
        if (category.Value.Equals(CarCategory.Business.Value))
            return 3.0;
        throw new NonExistingCategoryException(category);
    }
}