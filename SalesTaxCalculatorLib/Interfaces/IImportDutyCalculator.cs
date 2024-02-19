using SalesTaxCalculatorLib.Models;

namespace SalesTaxCalculatorLib.Interfaces;

public interface IImportDutyCalculator
{
    decimal CalculateImportDuty(Item item);
}
