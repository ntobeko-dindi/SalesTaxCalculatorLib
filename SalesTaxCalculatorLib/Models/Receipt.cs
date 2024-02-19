namespace SalesTaxCalculatorLib.Models;

public class Receipt
{
    public List<ReceiptItem> Items { get; set; } = new List<ReceiptItem>();
    public decimal SalesTaxes { get; set; }
    public decimal Total { get; set; }
}
