using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace RobotWarsKata.Tests
{
    [TestFixture]
    public class ArenaTests
    {
        private Mock<Arena> _objectUnderTest;
        private Arena Instance => _objectUnderTest.Object;

        [SetUp]
        public void Setup()
        {
            _objectUnderTest = new Mock<Arena>(5, 5)
            { CallBase = true };
        }

        #region MoveRobot

        [Test]
        public void MoveRobot_ShouldMoveRobot()
        {
            //arrange
            const int xPosition = 1;
            const int yPosition = 1;
            const string moves = "LMRLM";
            var robot = new Robot(xPosition, yPosition, Direction.E);

            Instance.Robots.Add(robot);

            var robotCommand = new RobotCommand(new Position(xPosition, yPosition, Direction.E), moves);

            //act
            Instance.MoveRobot(robotCommand);

            //assert
            robot.YPosition.Should().NotBe(xPosition);
        }

        [Test]
        public void MoveRobot_WhenNotRobotFound_ShouldThrowException()
        {
            //act
            Action action = () => Instance.MoveRobot(new RobotCommand(new Position(1, 1, Direction.E), "LMRLML"));

            //assert
            action.Should().Throw<RobotWarsException>().And.Message.Should().Be(Instance.NoRobot);
        }

        #endregion

        #region GetRobotPositions

        [Test]
        public void GetRobotPositions()
        {
            var mockRobot = new Mock<Robot>(1, 1, Direction.E);
            Instance.Robots.Add(mockRobot.Object);
            mockRobot.Setup(x => x.GetPosition())
                .Returns(new Position(1, 1, Direction.E));

            var mockRobot2 = new Mock<Robot>(2, 3, Direction.W);
            Instance.Robots.Add(mockRobot2.Object);
            mockRobot2.Setup(x => x.GetPosition())
                .Returns(new Position(2, 3, Direction.W));

            var expected = new List<string>
            {
                "1 1 E",
                "2 3 W"
            };

            //act
            var actual = Instance.GetRobotPositions();

            //assert
            actual.Should().BeEquivalentTo(expected);

        }

        [Test]
        public void GetRobotPositions_ShouldGetPositionForEachRobotInTheArena()
        {
            //arrange
            var mockRobot = new Mock<Robot>(1, 1, Direction.E);
            Instance.Robots.Add(mockRobot.Object);
            mockRobot.Setup(x => x.GetPosition())
                .Returns(new Position(1, 1, Direction.E));

            var mockRobot2 = new Mock<Robot>(2, 3, Direction.W);
            Instance.Robots.Add(mockRobot2.Object);
            mockRobot2.Setup(x => x.GetPosition())
                .Returns(new Position(2, 3, Direction.W));

            //act
            Instance.GetRobotPositions();

            mockRobot.Verify(x => x.GetPosition(), Times.Once);
            mockRobot2.Verify(x => x.GetPosition(), Times.Once);
        }

        #endregion
    }
}
