using SalesTaxCalculatorLib.Enums;
using SalesTaxCalculatorLib.Models;
using SalesTaxCalculatorLib.Constants;
using SalesTaxCalculatorLib.Utilities;
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

        decimal basicTax = item.Price * TaxRates.BasicTaxRate;

        var roundedBasicTax = TaxUtility.RoundUpTax(basicTax);

        return roundedBasicTax;
    }
}
