using NSubstitute;
using NUnit.Framework;
using SalesTaxCalculatorLib.Enums;
using SalesTaxCalculatorLib.Models;
using SalesTaxCalculatorLib.Services;
using SalesTaxCalculatorLib.Interfaces;

namespace SalesTaxCalculatorLib.Tests.ReceiptGeneratorTests;

[TestFixture]
public class ReceiptGeneratorTests
{
    private ITaxCalculator _basicTaxCalculator;
    private ITaxCalculator _importDutyTaxCalculator;
    private IReceiptGenerator _receiptGenerator;

    [SetUp]
    public void Setup()
    {
        _basicTaxCalculator = Substitute.For<ITaxCalculator>();
        _importDutyTaxCalculator = Substitute.For<ITaxCalculator>();

        _receiptGenerator = new ReceiptGenerator(_basicTaxCalculator, _importDutyTaxCalculator);
    }

    [Test]
    public void GenerateReceipt_ForInput1_CalculatesCorrectTotals()
    {
        //--------------------Arrange-------------------------
        var items = new List<Item>
        {
            new() { Name = "Book", Price = 12.49m, Type = ItemTypeEnum.Book },
            new() { Name = "Music CD", Price = 14.99m, Type = ItemTypeEnum.Other },
            new() { Name = "Chocolate Bar", Price = 0.85m, Type = ItemTypeEnum.Food }
        };

        _basicTaxCalculator.CalculateTax(Arg.Any<Item>()).Returns(x => ((Item)x[0]).Name == "Music CD" ? 1.50m : 0m);

        _importDutyTaxCalculator.CalculateTax(Arg.Any<Item>()).Returns(0m);

        //--------------------Act-----------------------------
        var receipt = _receiptGenerator.GenerateReceipt(items);
        
        //--------------------Assert--------------------------
        Assert.Multiple(() =>
        {
            Assert.That(receipt.Total, Is.EqualTo(29.83m));
            Assert.That(receipt.SalesTaxes, Is.EqualTo(1.50m));

            Assert.That(receipt.Items[0].ShelfPrice, Is.EqualTo(12.49m));
            Assert.That(receipt.Items[1].ShelfPrice, Is.EqualTo(16.49m));
            Assert.That(receipt.Items[2].ShelfPrice, Is.EqualTo(0.85m));
        });
    }

    [Test]
    public void GenerateReceipt_ForInput2_CalculatesCorrectTotals()
    {
        //--------------------Arrange-------------------------
        var items = new List<Item>
        {
            new() { Name = "Imported box of chocolates", Price = 10.00m, IsImported = true, Type = ItemTypeEnum.Food },
            new() { Name = "Imported bottle of perfume", Price = 47.50m, IsImported = true, Type = ItemTypeEnum.Other }
        };

        _basicTaxCalculator.CalculateTax(Arg.Any<Item>()).Returns(x => ((Item)x[0]).Name == "Imported bottle of perfume" ? 4.75m : 0m);

        _importDutyTaxCalculator.CalculateTax(Arg.Any<Item>()).Returns(x => ((Item)x[0]).IsImported ? ((Item)x[0]).Price * 0.05m : 0m);

        //--------------------Act-----------------------------
        var receipt = _receiptGenerator.GenerateReceipt(items);

        //--------------------Assert--------------------------
        Assert.Multiple(() =>
        {
            Assert.That(receipt.Total, Is.EqualTo(65.15m));
            Assert.That(receipt.SalesTaxes, Is.EqualTo(7.65m));

            Assert.That(receipt.Items[0].ShelfPrice, Is.EqualTo(10.50m));
            Assert.That(receipt.Items[1].ShelfPrice, Is.EqualTo(54.65m));
        });
    }

    [Test]
    public void GenerateReceipt_ForInput3_CalculatesCorrectTotals()
    {
        //--------------------Arrange-------------------------
        var items = new List<Item>
        {
            new() { Name = "Imported bottle of perfume", Price = 27.99m, IsImported = true, Type = ItemTypeEnum.Other },
            new() { Name = "Bottle of perfume", Price = 18.99m, Type = ItemTypeEnum.Other },
            new() { Name = "Packet of headache pills", Price = 9.75m, Type = ItemTypeEnum.Medical },
            new() { Name = "Box of imported chocolates", Price = 11.25m, IsImported = true, Type = ItemTypeEnum.Food }
        };

        _basicTaxCalculator.CalculateTax(Arg.Any<Item>()).Returns(x => 
        ((Item)x[0]).Name == "Imported bottle of perfume" || ((Item)x[0]).Name == "Bottle of perfume" ? ((Item)x[0]).Price * 0.1m : 0m);

        _importDutyTaxCalculator.CalculateTax(Arg.Any<Item>()).Returns(x => ((Item)x[0]).IsImported ? ((Item)x[0]).Price * 0.05m : 0m);

        //--------------------Act-----------------------------
        var receipt = _receiptGenerator.GenerateReceipt(items);
        
        //--------------------Assert--------------------------
        Assert.Multiple(() =>
        {
            Assert.That(receipt.Total, Is.EqualTo(74.68m));
            Assert.That(receipt.SalesTaxes, Is.EqualTo(6.70m));

            Assert.That(receipt.Items[0].ShelfPrice, Is.EqualTo(32.19m));
            Assert.That(receipt.Items[1].ShelfPrice, Is.EqualTo(20.89m));
            Assert.That(receipt.Items[2].ShelfPrice, Is.EqualTo(9.75m));
            Assert.That(receipt.Items[3].ShelfPrice, Is.EqualTo(11.85m));
        });
    }
}