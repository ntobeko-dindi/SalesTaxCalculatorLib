using NUnit.Framework;
using SalesTaxCalculatorLib.Enums;
using SalesTaxCalculatorLib.Models;
using SalesTaxCalculatorLib.Services;
using SalesTaxCalculatorLib.Interfaces;

namespace SalesTaxCalculatorLib.Tests.TaxCalculatorsTests;

[TestFixture]
public class ImportDutyCalculatorTests
{
    private ITaxCalculator _importDutyCalculator;

    [SetUp]
    public void Setup()
    {
        _importDutyCalculator = new ImportDutyCalculator();
    }

    [Test]
    public void CalculateImportDuty_OnImportedItem_ChargesDuty()
    {
        //--------------------Arrange-------------------------
        var item = new Item
        {
            Name = "Imported bottle of perfume",
            Price = 27.99m,
            IsImported = true,
            Type = ItemTypeEnum.Other
        };

        var expectedValue = 1.40m;

        //--------------------Act-----------------------------
        var duty = _importDutyCalculator.CalculateTax(item);

        // -------------------Assert--------------------------
        Assert.That(duty, Is.EqualTo(expectedValue));
    }

    [Test]
    public void CalculateImportDuty_OnNonImportedItem_NoDuty()
    {
        //--------------------Arrange-------------------------
        var item = new Item
        {
            Name = "Local Bottle of perfume",
            Price = 18.99m,
            IsImported = false,
            Type = ItemTypeEnum.Other
        };

        var expectedValue = 0;

        //--------------------Act-----------------------------
        var duty = _importDutyCalculator.CalculateTax(item);

        // -------------------Assert--------------------------
        Assert.That(duty, Is.EqualTo(expectedValue));
    }

    [Test]
    public void CalculateImportDuty_OnImportedExemptItem_ChargesDuty()
    {
        //--------------------Arrange-------------------------
        var item = new Item
        {
            Name = "Imported box of chocolates",
            Price = 11.25m,
            IsImported = true,
            Type = ItemTypeEnum.Food
        };

        var expectedValue = 0.60m;

        //--------------------Act-----------------------------
        var duty = _importDutyCalculator.CalculateTax(item);

        // -------------------Assert--------------------------
        Assert.That(duty, Is.EqualTo(expectedValue));
    }

    [Test]
    public void CalculateImportDuty_OnImportedBook_ChargesDuty()
    {
        //--------------------Arrange-------------------------
        var item = new Item
        {
            Name = "Imported book",
            Price = 12.49m,
            IsImported = true,
            Type = ItemTypeEnum.Book
        };

        var expectedValue = 0.65m;

        //--------------------Act-----------------------------
        var duty = _importDutyCalculator.CalculateTax(item);

        //--------------------Assert--------------------------
        Assert.That(duty, Is.EqualTo(expectedValue));
    }

    [Test]
    public void CalculateImportDuty_OnImportedMedicalProduct_ChargesDuty()
    {
        //--------------------Arrange-------------------------
        var item = new Item
        {
            Name = "Imported packet of headache pills",
            Price = 9.75m,
            IsImported = true,
            Type = ItemTypeEnum.Medical
        };

        var expectedValue = 0.50m;

        //--------------------Act-----------------------------
        var duty = _importDutyCalculator.CalculateTax(item);

        //--------------------Assert--------------------------
        Assert.That(duty, Is.EqualTo(expectedValue));
    }
}