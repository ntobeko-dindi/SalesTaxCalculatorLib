using SalesTaxCalculatorLib.Constants;

namespace SalesTaxCalculatorLib.Utilities;

public static class TaxUtility
{
    public static decimal RoundUpTax(decimal amount)
    {
        return Math.Ceiling(amount / RoundingConstants.TaxRoundingFactor) * RoundingConstants.TaxRoundingFactor;
    }
}
