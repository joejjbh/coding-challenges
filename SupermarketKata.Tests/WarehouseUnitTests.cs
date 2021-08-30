using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using SupermarketKata.Models;

namespace SupermarketKata.Tests
{
    [TestFixture]
    public class WarehouseUnitTests
    {
        private Mock<Warehouse> _objectUnderTest;
        private Warehouse Instance => _objectUnderTest.Object;

        [SetUp]
        public void Setup()
        {
            _objectUnderTest = new Mock<Warehouse>(new Dictionary<string, Item>())
            { CallBase = true };
        }

        #region Constructor

        [Test]
        public void Constructor()
        {
            //act
            var items = new Dictionary<string, Item>
            {
                {
                    "A",
                    new Item("A", 23m)
                }
            };
            var actual = new Warehouse(items);

            //assert
            actual.Should().NotBeNull();
            actual.Items.Should().BeEquivalentTo(items);
        }

        [Test]
        public void Constructor_WhenItemsGivenAreNull_ReturnsException()
        {
            //act
            Action action = () => new Warehouse(null);

            //assert
            action.Should().Throw<ArgumentNullException>("items");
        }

        #endregion

        #region GetItem

        [Test]
        public void GetItem_WhenGivenItemThatExists_ShouldReturnItem()
        {
            //arrange
            const string sku = "A";
            var expected = new Item(sku, 23m);
            Instance.Items.Add(sku, expected);
            Instance.Items.Add("Not A", new Item("Not A", 10m));

            //act
            var actual = Instance.GetItem(expected.StockKeepingUnit);

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void GetItem_WhenGivenItemThatDoesNotExist_ShouldReturnNull()
        {
            //act
            var actual = Instance.GetItem("A");

            //assert
            actual.Should().Be(null);
        }

        #endregion

        #region SaveItem

        [Test]
        public void SaveItem()
        {
            //arrange
            const string sku = "A";
            var item = new Item(sku, 50m, 3, 130m);

            //act

            Instance.SaveItem(item);

            //assert
            Instance.Items.Count.Should().Be(1);
            Instance.Items[sku].Should().Be(item);
        }

        [Test]
        public void SaveItem_WhenSavingItemThatAlreadyExists_ShouldThrowException()
        {
            //arrange
            var item = new Item("A", 23m);

            Instance.Items.Add("A", item);

            //act
            Action action = () => Instance.SaveItem(item);

            //assert
            action.Should().Throw<SupermarketKataException>().And.Message.Should().Be(Instance.SkuAlreadyExistsError);
        }

        #endregion

        #region UpdateItem

        [Test]
        public void UpdateItem()
        {
            //arrange
            const string sku = "A";
            Instance.Items.Add(sku, new Item(sku, 10m, 3, 20m));

            var expected = new Item(sku, 15m, 2, 25m);

            //act
            Instance.UpdateItem(expected);

            //assert
            Instance.Items[sku].Should().Be(expected);
        }

        [Test]
        public void UpdateItem_GivenItemThatDoesNotExist_ShouldThrowException()
        {
            //arrange
            var item = new Item("A", 10m);

            //act
            Action action = () => Instance.UpdateItem(item);

            //assert
            action.Should().Throw<Exception>().And.Message.Should().Be(Instance.ItemDoesNotExistError);

        }

        #endregion

        #region GetItemSubTotal

        [TestCase(50, 130, 3, 3, 130)]
        [TestCase(50, 130, 3, 4, 180)]
        [TestCase(50, 130, 3, 2, 100)]
        [TestCase(50, 130, 3, 0, 0)]
        public void GetItemSubTotal_WhenItemHasSpecialOffer_ShouldReturnTotal(decimal price, decimal specialOfferPrice,
            int specialOfferQuantity, int quantity, decimal expected)
        {
            //arrange
            const string sku = "A";

            var item = new Item(sku, price, specialOfferQuantity, specialOfferPrice);
            Instance.Items.Add(sku, item);

            //act
            var actual = Instance.GetItemSubTotal(sku, quantity);

            //assert
            actual.Should().Be(expected);
        }

        [TestCase(10.0, 3, 30)]
        [TestCase(10.0, 0, 0)]
        [TestCase(10.0, 1, 10)]
        public void GetItemSubTotal_WhenItemDoesNotHaveSpecialOffer_ShouldReturnTotal(decimal price, int quantity,
            decimal expected)
        {
            //arrange
            const string sku = "A";

            var item = new Item(sku, price);
            Instance.Items.Add(sku, item);

            //act
            var actual = Instance.GetItemSubTotal(sku, quantity);

            //assert
            actual.Should().Be(expected);
        }

        #endregion
    }
}
