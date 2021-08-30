namespace RobotWarsKata
{
    public class Position
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public Direction Direction { get; set; }

        public Position(int xPosition, int yPosition, Direction direction)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Direction = direction;
        }
    }
}