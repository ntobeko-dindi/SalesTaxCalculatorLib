using SalesTaxCalculatorLib.Interfaces;
using SalesTaxCalculatorLib.Models;

namespace SalesTaxCalculatorLib.Services;

public class ReceiptGenerator : IReceiptGenerator
{
    private readonly ITaxCalculator _basicTaxCalculator;
    private readonly ITaxCalculator _importDutyTaxCalculator;

    public ReceiptGenerator(ITaxCalculator basicTaxCalculator, ITaxCalculator importDutyTaxCalculator)
    {
        _basicTaxCalculator = basicTaxCalculator;
        _importDutyTaxCalculator = importDutyTaxCalculator;
    }

    public Receipt GenerateReceipt(List<Item> items)
    {
        throw new NotImplementedException();
    }
}
