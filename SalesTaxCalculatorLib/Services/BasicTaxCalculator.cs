using SalesTaxCalculatorLib.Enums;
using SalesTaxCalculatorLib.Models;
using SalesTaxCalculatorLib.Constants;
using SalesTaxCalculatorLib.Interfaces;

namespace SalesTaxCalculatorLib.Services;

public class BasicTaxCalculator : ITaxCalculator
{
    public decimal CalculateTax(Item item)
    {
        if (item.Type == ItemTypeEnum.Book || item.Type == ItemTypeEnum.Food || item.Type == ItemTypeEnum.Medical)
        {
            return 0m; 
        }

        decimal basicTax = item.ShelfPrice * TaxRates.BasicTaxRate;

        var roundedBasicTax = RoundUpTax(basicTax);

        return roundedBasicTax;
    }

    private decimal RoundUpTax(decimal tax)
    {
        return Math.Ceiling(tax / RoundingConstants.TaxRoundingFactor) * RoundingConstants.TaxRoundingFactor;
    }
}
