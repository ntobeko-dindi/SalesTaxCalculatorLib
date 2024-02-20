using SalesTaxCalculatorLib.Models;
using SalesTaxCalculatorLib.Constants;
using SalesTaxCalculatorLib.Utilities;
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

        var roundedImportDutyTax = TaxUtility.RoundUpTax(importDutyTax);

        return roundedImportDutyTax;
    }
}
