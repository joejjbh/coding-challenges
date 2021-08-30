namespace RobotWarsKata
{
    public interface IRobot
    {
        void ProcessMoves(string moves);

        Position GetPosition();
    }
}