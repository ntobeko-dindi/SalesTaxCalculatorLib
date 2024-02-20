using SalesTaxCalculatorLib.Models;
using SalesTaxCalculatorLib.Constants;
using SalesTaxCalculatorLib.Interfaces;

namespace SalesTaxCalculatorLib.Services;

public class ImportDutyCalculator : ITaxCalculator
{
    public decimal CalculateTax(Item item)
    {
        if (!item.IsImported)
        {
            return 0m;
        }

        decimal importDutyTax = item.ShelfPrice * TaxRates.ImportDuty;

        return RoundUpTax(importDutyTax);
    }

    private decimal RoundUpTax(decimal tax)
    {
        return Math.Ceiling(tax / RoundingConstants.TaxRoundingFactor) * RoundingConstants.TaxRoundingFactor;
    }
}
