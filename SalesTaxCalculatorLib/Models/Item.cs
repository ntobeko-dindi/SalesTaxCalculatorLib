using SalesTaxCalculatorLib.Enums;

namespace SalesTaxCalculatorLib.Models;

public class Item
{
    public string Name { get; set; }
    public decimal ShelfPrice { get; set; }
    public bool IsImported { get; set; }
    public ItemTypeEnum Type { get; set; }
}