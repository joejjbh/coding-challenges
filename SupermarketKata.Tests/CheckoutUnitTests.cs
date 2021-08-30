using FluentAssertions;
using Moq;
using NUnit.Framework;
using SupermarketKata.Interfaces;
using System;
using System.Collections.Generic;
using SupermarketKata.Models;

namespace SupermarketKata.Tests
{
    [TestFixture]
    public class CheckoutUnitTests
    {
        private Mock<Checkout> _objectUnderTest;
        private Mock<IWarehouse> _mockWarehouse;
        private Checkout Instance => _objectUnderTest.Object;

        [SetUp]
        public void Setup()
        {
            _mockWarehouse = new Mock<IWarehouse>();
            _objectUnderTest = new Mock<Checkout>(_mockWarehouse.Object)
            { CallBase = true };
            
            Instance.ScannedItems = new Dictionary<string, int>();
        }

        #region Constructor

        [Test]
        public void Constructor()
        {
            //act
            var actual = new Checkout(_mockWarehouse.Object);

            //assert
            actual.Should().NotBeNull();
            actual.Warehouse.Should().BeEquivalentTo(_mockWarehouse.Object);
            actual.ScannedItems.Should().BeOfType<Dictionary<string, int>>();
        }

        [Test]
        public void Constructor_WhenWarehouseIsNull_ReturnsException()
        {
            //act
            Action action = () => new Checkout(null);

            //assert
            action.Should().Throw<ArgumentNullException>("warehouse");
        }

        #endregion

        #region Scan

        [Test]
        public void Scan_GivenItemThatExistsAndHasNotBeenAddedBefore_ShouldAddItemToScannedItems()
        {
            const string sku = "A";
            _mockWarehouse.Setup(x => x.GetItem(sku)).Returns(new Item(sku, 23m));

            //act
            Instance.Scan(sku);

            //assert
            Instance.ScannedItems.Count.Should().Be(1);
            Instance.ScannedItems[sku].Should().Be(1);
        }

        [Test]
        public void Scan_WhenGivenItemThatDoesNotExistInScannedItems_ShouldLookupItemByCallingWarehouse()
        {
            //arrange
            const string sku = "A";
            _mockWarehouse.Setup(x => x.GetItem(sku)).Returns(new Item(sku, 23m));

            //act
            Instance.Scan(sku);

            //assert
            _mockWarehouse.Verify(x => x.GetItem(sku), Times.Once);
        }

        [Test]
        public void Scan_WhenGivenItemThatExistsInScannedItems_ShouldAddOneToItsCount()
        {
            //arrange
            const string existingSku = "A";
            Instance.ScannedItems.Add(existingSku, 1);

            var initialCount = Instance.ScannedItems[existingSku];

            //act
            Instance.Scan(existingSku);

            //assert
            Instance.ScannedItems[existingSku].Should().Be(initialCount + 1);
        }

        [Test]
        public void Scan_WhenItemGivenDoesNotExistInWarehouse_ShouldThrowException()
        {
            //arrange
            const string sku = "This does not exist";
            _mockWarehouse.Setup(x => x.GetItem(sku)).Returns((Item)null);

            //act
            Action action = () => Instance.Scan("This does not exist");

            //assert
            action.Should().Throw<SupermarketKataException>().And.Message.Should().Be(Instance.SkuNotExistError);
        }

        #endregion

        #region GetTotalPrice

        [Test]
        public void GetTotalPrice()
        {
            //arrange
            Instance.ScannedItems.Add("A", 2);
            Instance.ScannedItems.Add("B", 3);

            const decimal mockedSubTotal = 10m;
            _mockWarehouse.Setup(x =>
                x.GetItemSubTotal(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(mockedSubTotal);

            //act
            var actual = Instance.GetTotalPrice();

            //assert
            actual.Should().Be(Instance.ScannedItems.Count * mockedSubTotal);
        }

        [Test]
        public void GetTotalPrice_ShouldGetSubTotalForEachScannedItem()
        {
            //arrange
            const string sku = "A";
            const int quantity = 1;

            Instance.ScannedItems.Add("A", 1);
            Instance.ScannedItems.Add("B", 3);

            //act
            Instance.GetTotalPrice();

            //assert
            _mockWarehouse.Verify(x =>
                x.GetItemSubTotal(sku, quantity), 
                Times.Exactly(Instance.ScannedItems.Count));
        }

        [Test]
        public void GetTotalPrice_WhenNoScannedItems_ShouldReturn0()
        {
            //act
            var actual = Instance.GetTotalPrice();

            //assert
            actual.Should().Be(0m);
        }

        #endregion
    }
}
