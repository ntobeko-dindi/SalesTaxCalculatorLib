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
            new() { Name = "Book", ShelfPrice = 12.49m, Type = ItemTypeEnum.Book },
            new() { Name = "Music CD", ShelfPrice = 14.99m, Type = ItemTypeEnum.Other },
            new() { Name = "Chocolate Bar", ShelfPrice = 0.85m, Type = ItemTypeEnum.Food }
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
        });
    }

    [Test]
    public void GenerateReceipt_ForInput2_CalculatesCorrectTotals()
    {
        //--------------------Arrange-------------------------
        var items = new List<Item>
        {
            new() { Name = "Imported box of chocolates", ShelfPrice = 10.00m, IsImported = true, Type = ItemTypeEnum.Food },
            new() { Name = "Imported bottle of perfume", ShelfPrice = 47.50m, IsImported = true, Type = ItemTypeEnum.Other }
        };

        _basicTaxCalculator.CalculateTax(Arg.Any<Item>()).Returns(x => ((Item)x[0]).Name == "Imported bottle of perfume" ? 4.75m : 0m);

        _importDutyTaxCalculator.CalculateTax(Arg.Any<Item>()).Returns(x => ((Item)x[0]).IsImported ? ((Item)x[0]).ShelfPrice * 0.05m : 0m);

        //--------------------Act-----------------------------
        var receipt = _receiptGenerator.GenerateReceipt(items);

        //--------------------Assert--------------------------
        Assert.Multiple(() =>
        {
            Assert.That(receipt.Total, Is.EqualTo(65.15m));
            Assert.That(receipt.SalesTaxes, Is.EqualTo(7.65m));
        });
    }

    [Test]
    public void GenerateReceipt_ForInput3_CalculatesCorrectTotals()
    {
        //--------------------Arrange-------------------------
        var items = new List<Item>
        {
            new() { Name = "Imported bottle of perfume", ShelfPrice = 27.99m, IsImported = true, Type = ItemTypeEnum.Other },
            new() { Name = "Bottle of perfume", ShelfPrice = 18.99m, Type = ItemTypeEnum.Other },
            new() { Name = "Packet of headache pills", ShelfPrice = 9.75m, Type = ItemTypeEnum.Medical },
            new() { Name = "Box of imported chocolates", ShelfPrice = 11.25m, IsImported = true, Type = ItemTypeEnum.Food }
        };

        _basicTaxCalculator.CalculateTax(Arg.Any<Item>()).Returns(x => 
        ((Item)x[0]).Name == "Imported bottle of perfume" || ((Item)x[0]).Name == "Bottle of perfume" ? ((Item)x[0]).ShelfPrice * 0.1m : 0m);

        _importDutyTaxCalculator.CalculateTax(Arg.Any<Item>()).Returns(x => ((Item)x[0]).IsImported ? ((Item)x[0]).ShelfPrice * 0.05m : 0m);

        //--------------------Act-----------------------------
        var receipt = _receiptGenerator.GenerateReceipt(items);

        //--------------------Assert--------------------------
        Assert.That(receipt.Total, Is.EqualTo(74.68m));
        Assert.That(receipt.SalesTaxes, Is.EqualTo(6.70m));
    }
}