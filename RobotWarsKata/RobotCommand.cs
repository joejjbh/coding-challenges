namespace RobotWarsKata
{
    public class RobotCommand
    {
        public Position StartingPosition { get; set; }
        public string Moves { get; set; }

        public RobotCommand(Position startingPosition, string moves)
        {
            StartingPosition = startingPosition;
            Moves = moves;
        }
    }
}