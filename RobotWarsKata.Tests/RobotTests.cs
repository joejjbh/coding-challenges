using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace RobotWarsKata.Tests
{
    [TestFixture]
    public class RobotTests
    {
        private Mock<Robot> _objectUnderTest;
        private Robot Instance => _objectUnderTest.Object;

        [SetUp]
        public void Setup()
        {
            _objectUnderTest = new Mock<Robot>
                {CallBase = true};
        }

        #region ProcessMoves

        [Test]
        public void ProcessMoves_ShouldCallMoveForEachMoveInString()
        {
            //arrange
            const string moves = "LRMLRM";

            //act
            Instance.ProcessMoves(moves);

            //assert
            _objectUnderTest.Verify(x => x.Move(It.IsAny<char>()), Times.Exactly(moves.Length));
        }

        #endregion

        #region Move

        [Test]
        public void Move_WhenGivenMoveIsL_ShouldCallTurnLeft()
        {
            //act
            Instance.Move('L');

            //assert
            _objectUnderTest.Verify(x => x.TurnLeft(), Times.Once);
        }

        [Test]
        public void Move_WhenGivenMoveIsR_ShouldCallTurnRight()
        {
            //act
            Instance.Move('R');

            //assert
            _objectUnderTest.Verify(x => x.TurnRight(), Times.Once);
        }

        [Test]
        public void Move_WhenGivenMoveIsM_ShouldCallMoveForward()
        {
            //act
            Instance.Move('M');

            //assert
            _objectUnderTest.Verify(x => x.MoveForward(), Times.Once);
        }

        #endregion

        #region TurnLeft

        [TestCase(Direction.N, Direction.W)]
        [TestCase(Direction.E, Direction.N)]
        [TestCase(Direction.S, Direction.E)]
        [TestCase(Direction.W, Direction.S)]
        public void TurnLeft_ShouldFaceCorrectDirection(Direction startDirection, Direction expectedDirection)
        {
            //arrange
            Instance.FacingDirection = startDirection;

            //act
            Instance.TurnLeft();

            //assert
            Instance.FacingDirection.Should().Be(expectedDirection);
        }

        #endregion

        #region TurnRight

        [TestCase(Direction.N, Direction.E)]
        [TestCase(Direction.E, Direction.S)]
        [TestCase(Direction.S, Direction.W)]
        [TestCase(Direction.W, Direction.N)]
        public void TurnRight_ShouldFaceCorrectDirection(Direction startDirection, Direction expectedDirection)
        {
            //arrange
            Instance.FacingDirection = startDirection;

            //act
            Instance.TurnRight();

            //assert
            Instance.FacingDirection.Should().Be(expectedDirection);
        }

        #endregion

        #region MoveForward

        [TestCase(Direction.N, 1, 1, 1, 2)]
        [TestCase(Direction.E, 1, 1, 2, 1)]
        [TestCase(Direction.S, 1, 1, 1, 0)]
        [TestCase(Direction.W, 1, 1, 0, 1)]
        public void MoveForward_ShouldGoToCorrectPosition(Direction startDirection, int startX, int startY, int endX, int endY)
        {
            //arrange
            Instance.FacingDirection = startDirection;
            Instance.XPosition = startX;
            Instance.YPosition = startY;

            //act
            Instance.MoveForward();

            //assert
            Instance.XPosition.Should().Be(endX);
            Instance.YPosition.Should().Be(endY);
        }

        #endregion

        #region GetPosition

        [Test]
        public void GetPosition_ShouldReturnPosition()
        {
            //arrange
            var expectedPosition = new Position(3, 2, Direction.N);
            Instance.FacingDirection = expectedPosition.Direction;
            Instance.XPosition = expectedPosition.XPosition;
            Instance.YPosition = expectedPosition.YPosition;

            //act
            var actual = Instance.GetPosition();

            //assert
            actual.Should().BeEquivalentTo(expectedPosition);
        }

        #endregion
    }
}
