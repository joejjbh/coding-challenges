using System;
using FluentAssertions;
using NUnit.Framework;
using SupermarketKata.Models;

namespace SupermarketKata.Tests
{
    [TestFixture]
    public class ItemUnitTests
    {

        #region Constructor

        [Test]
        public void Constructor_WhenGivenInvalidPrice_ReturnsException()
        {
            //act
            Action action = () => new Item("A", -1, 2, 20m);

            //assert
            action.Should().Throw<ArgumentOutOfRangeException>("price");
        }

        [Test]
        public void Constructor_WhenGivenInvalidOfferQuantity_ReturnsException()
        {
            //act
            Action action = () => new Item("A", -1, -1, 20m);

            //assert
            action.Should().Throw<ArgumentOutOfRangeException>("specialOfferQuantity");
        }

        [Test]
        public void Constructor_WhenGivenInvalidOfferPrice_ReturnsException()
        {
            //act
            Action action = () => new Item("A", -1, 2, -1);

            //assert
            action.Should().Throw<ArgumentOutOfRangeException>("specialOfferPrice");
        }

        #endregion

        #region HasSpecialOffer

        [TestCase(3, 10, true)]
        [TestCase(0, 0, false)]
        public void HasSpecialOffer_ShouldGetSetCorrectly(decimal specialOfferPrice, int specialOfferQuantity, bool expected)
        {
            //act
            var item = new Item("A", 2, specialOfferQuantity, specialOfferPrice);

            //assert
            item.HasSpecialOffer.Should().Be(expected);
        }

        #endregion

    }
}
