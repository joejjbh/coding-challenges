namespace RobotWarsKata
{
    public class Robot : IRobot
    {
        public int XPosition { get; set; }
        public Direction FacingDirection { get; set; }
        public int YPosition { get; set; }

        public Robot(int xPosition, int yPosition, Direction direction)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            FacingDirection = direction;
        }

        public Robot()
        {
            XPosition = 0;
            YPosition = 0;
            FacingDirection = Direction.N;
        }

        public void ProcessMoves(string moves)
        {
            var movesAsChar = moves.ToCharArray();
            foreach (var move in movesAsChar)
            {
                Move(move);
            }
        }

        protected internal virtual void Move(char move)
        {
            switch (move)
            {
                case 'L':
                    TurnLeft();
                    break;
                case 'M':
                    MoveForward();
                    break;
                case 'R':
                    TurnRight();
                    break;
            }
        }

        protected internal virtual void TurnLeft()
        {
            switch (FacingDirection)
            {
                case Direction.N:
                    FacingDirection = Direction.W;
                    break;
                case Direction.E:
                    FacingDirection = Direction.N;
                    break;
                case Direction.S:
                    FacingDirection = Direction.E;
                    break;
                case Direction.W:
                    FacingDirection = Direction.S;
                    break;
            }
        }

        protected internal virtual void TurnRight()
        {
            switch (FacingDirection)
            {
                case Direction.N:
                    FacingDirection = Direction.E;
                    break;
                case Direction.E:
                    FacingDirection = Direction.S;
                    break;
                case Direction.S:
                    FacingDirection = Direction.W;
                    break;
                case Direction.W:
                    FacingDirection = Direction.N;
                    break;
            }
        }

        protected internal virtual void MoveForward()
        {
            switch (FacingDirection)
            {
                case Direction.N:
                    YPosition += 1;
                    break;
                case Direction.E:
                    XPosition += 1;
                    break;
                case Direction.S:
                    YPosition -= 1;
                    break;
                case Direction.W:
                    XPosition -= 1;
                    break;
            }
        }

        public virtual Position GetPosition()
        {
            return new Position(XPosition, YPosition, FacingDirection);
        }
    }
}
