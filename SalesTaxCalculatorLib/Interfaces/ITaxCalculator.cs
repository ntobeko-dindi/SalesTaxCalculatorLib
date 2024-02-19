using SalesTaxCalculatorLib.Models;

namespace SalesTaxCalculatorLib.Interfaces;

public interface ITaxCalculator
{
    decimal CalculateTax(Item item);
}
