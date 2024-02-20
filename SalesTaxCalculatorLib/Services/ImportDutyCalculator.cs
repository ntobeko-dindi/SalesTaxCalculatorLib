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
            return TaxRates.ExemptTaxRate;
        }

        decimal importDutyTax = item.Price * TaxRates.ImportDuty;

        var roundedImportDutyTax = TaxUtility.RoundUpTax(importDutyTax);

        return roundedImportDutyTax;
    }
}
