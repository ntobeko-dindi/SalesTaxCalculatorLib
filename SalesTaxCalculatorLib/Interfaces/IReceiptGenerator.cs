using SalesTaxCalculatorLib.Models;

namespace SalesTaxCalculatorLib.Interfaces;

public interface IReceiptGenerator
{
    Receipt GenerateReceipt(List<Item> items);
}
