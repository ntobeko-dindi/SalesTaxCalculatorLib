using SalesTaxCalculatorLib.Interfaces;
using SalesTaxCalculatorLib.Utilities;
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
        var receiptItems = new List<ReceiptItem>();
        var totalSalesTaxes = 0m;
        var totalAmount = 0m;

        foreach (var item in items)
        {
            var basicTax = _basicTaxCalculator.CalculateTax(item);
            var importDuty = _importDutyTaxCalculator.CalculateTax(item);

            var totalItemTax = TaxUtility.RoundUpTax(basicTax + importDuty);
            var itemTotalPrice = item.Price + totalItemTax;

            receiptItems.Add(new ReceiptItem { Name = item.Name, ShelfPrice = itemTotalPrice });

            totalSalesTaxes += totalItemTax;
            totalAmount += itemTotalPrice;
        }

        return new Receipt
        {
            Items = receiptItems,
            SalesTaxes = totalSalesTaxes,
            Total = totalAmount
        };
    }
}
