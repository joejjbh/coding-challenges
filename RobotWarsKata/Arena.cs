using System.Collections.Generic;

namespace RobotWarsKata
{
    public class Arena : IArena
    {
        private readonly int _arenaHeight;
        private readonly int _arenaWidth;
        public List<Robot> Robots { get; set; }

        protected internal string NoRobot => "No robot found for the given co-ordinates";

        public Arena(int arenaHeight, int arenaWidth)
        {
            _arenaHeight = arenaHeight;
            _arenaWidth = arenaWidth;
            Robots = new List<Robot>();
        }

        public void MoveRobot(RobotCommand robotCommand)
        {
            var robot = Robots.Find(x => x.XPosition == robotCommand.StartingPosition.XPosition && x.YPosition == robotCommand.StartingPosition.YPosition);
            if (robot == null) throw new RobotWarsException(NoRobot);
            robot.ProcessMoves(robotCommand.Moves);
        }

        public List<string> GetRobotPositions()
        {
            var positions = new List<string>();

            foreach (var robot in Robots)
            {
                var position = robot.GetPosition();
                positions.Add($"{position.XPosition} {position.YPosition} {GetDirectionAsString(position.Direction)}");
            }

            return positions;
        }

        protected internal virtual string GetDirectionAsString(Direction direction)
        {
            switch (direction)
            {
                case Direction.N:
                    return "N";
                case Direction.E:
                    return "E";
                case Direction.S:
                    return "S";
                case Direction.W:
                    return "W";
                default:
                    return "Unable to parse";
            }
        }
    }
}