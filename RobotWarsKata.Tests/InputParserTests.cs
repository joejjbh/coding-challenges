using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;

namespace RobotWarsKata.Tests
{
    [TestFixture]
    public class InputParserTests
    {
        private Mock<InputParser> _objectUnderTest;
        private InputParser Instance => _objectUnderTest.Object;

        [SetUp]
        public void Setup()
        {
            _objectUnderTest = new Mock<InputParser>()
            {
                CallBase = true
            };
        }

        #region ParseGameCommands

        [Test]
        public void ParseGameCommands()
        {
            //arrange
            var expectedPosition = new Position(1, 2, Direction.N);
            var expected = new GameCommand
            {
                Arena = new ArenaCommand(5, 5),
                Robots = new List<RobotCommand>
                {
                    new RobotCommand(expectedPosition, "LMLMLMLMM"),
                }
            };

            _objectUnderTest.Setup(x => x.ParsePosition(It.IsAny<string>())).Returns(expectedPosition);

            //act
            var actual = Instance.ParseGameCommands("5 5 \r\n 1 2 N \r\n LMLMLMLMM");

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        #endregion

        #region ParsePosition

        [TestCase("1 2 N", 1, 2, Direction.N)]
        [TestCase("3 3 E", 3, 3, Direction.E)]
        public void ParsePosition(string positionString, int expectedX, int expectedY, Direction expectedDirection)
        {
            //act
            var actual = Instance.ParsePosition(positionString);

            //assert
            actual.Should().BeEquivalentTo(new Position(expectedX, expectedY, expectedDirection));
        }

        #endregion
    }
}
