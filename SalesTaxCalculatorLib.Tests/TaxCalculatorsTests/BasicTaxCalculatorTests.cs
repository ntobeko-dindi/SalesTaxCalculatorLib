using NUnit.Framework;
using NUnit.Framework.Internal;
using SalesTaxCalculatorLib.Enums;
using SalesTaxCalculatorLib.Models;
using SalesTaxCalculatorLib.Services;
using SalesTaxCalculatorLib.Interfaces;

namespace SalesTaxCalculatorLib.Tests.TaxCalculatorsTests;

[TestFixture]
public class BasicTaxCalculatorTests
{
    private ITaxCalculator _basicTaxCalculator;

    [SetUp]
    public void Setup()
    {
        _basicTaxCalculator = new BasicTaxCalculator();
    }

    [Test]
    public void CalculateTax_OnBook_ShouldBeExemptFromBasicSalesTax()
    {
        //--------------------Arrange-------------------------
        var item = new Item
        {
            Name = "Book",
            ShelfPrice = 12.49m,
            IsImported = false,
            Type = ItemTypeEnum.Book
        };

        var expectedValue = 0m;

        //--------------------Act-----------------------------
        var tax = _basicTaxCalculator.CalculateTax(item);

        //-------------------Assert--------------------------
        Assert.That(tax, Is.EqualTo(expectedValue));
    }

    [Test]
    public void CalculateTax_OnMusicCD_ShouldChargeBasicSalesTax()
    {
        //--------------------Arrange-------------------------
        var item = new Item
        {
            Name = "Music CD",
            ShelfPrice = 14.99m,
            IsImported = false,
            Type = ItemTypeEnum.Other
        };

        var expectedValue = 1.50m;

        //--------------------Act-----------------------------
        var tax = _basicTaxCalculator.CalculateTax(item);

        //-------------------Assert--------------------------
        Assert.That(tax, Is.EqualTo(expectedValue));
    }

    [Test]
    public void CalculateTax_OnChocolateBar_ShouldBeExemptFromBasicSalesTax()
    {
        //--------------------Arrange-------------------------
        var item = new Item
        {
            Name = "Chocolate bar",
            ShelfPrice = 0.85m,
            IsImported = false,
            Type = ItemTypeEnum.Food
        };

        var expectedValue = 0m;

        //--------------------Act-----------------------------
        var tax = _basicTaxCalculator.CalculateTax(item);

        //-------------------Assert--------------------------
        Assert.That(tax, Is.EqualTo(expectedValue));
    }

    [Test]
    public void CalculateTax_OnPerfume_ShouldChargeBasicSalesTax()
    {
        //--------------------Arrange-------------------------
        var item = new Item
        {
            Name = "Perfume",
            ShelfPrice = 18.99m,
            IsImported = false,
            Type = ItemTypeEnum.Other
        };

        var expectedValue = 1.90m;

        //--------------------Act-----------------------------
        var tax = _basicTaxCalculator.CalculateTax(item);

        //-------------------Assert--------------------------
        Assert.That(tax, Is.EqualTo(expectedValue));
    }

    [Test]
    public void CalculateTax_OnPills_ShouldBeExemptFromBasicSalesTax()
    {
        //--------------------Arrange-------------------------
        var item = new Item
        {
            Name = "Packet of headache pills",
            ShelfPrice = 9.75m,
            IsImported = false,
            Type = ItemTypeEnum.Medical
        };

        var expectedValue = 0m;

        //--------------------Act-----------------------------
        var tax = _basicTaxCalculator.CalculateTax(item);

        //-------------------Assert--------------------------
        Assert.That(tax, Is.EqualTo(expectedValue));
    }
}