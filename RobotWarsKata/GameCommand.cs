using System.Collections.Generic;

namespace RobotWarsKata
{
    public class GameCommand
    {
        public ArenaCommand Arena { get; set; }

        public List<RobotCommand> Robots { get; set; }
    }
}