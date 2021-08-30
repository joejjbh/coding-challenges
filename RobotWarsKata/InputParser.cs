using System.Collections.Generic;

namespace RobotWarsKata
{
    public class InputParser
    {
        protected internal virtual GameCommand ParseGameCommands(string game)
        {
            var lines = game.Split("\r\n");
            var arenaCommands = lines[0].Split(" ");

            var robotCommands = new List<RobotCommand>();
            for (var i = 1; i < lines.Length; i += 2)
            {
                var position = ParsePosition(lines[i].Trim());

                var robotCommand = new RobotCommand(position, lines[i + 1].Trim());
                robotCommands.Add(robotCommand);
            }

            return new GameCommand
            {
                Arena = new ArenaCommand(int.Parse(arenaCommands[0]), int.Parse(arenaCommands[1])),
                Robots = robotCommands
            };
        }

        protected internal virtual Position ParsePosition(string positionAsString)
        {
            var components = positionAsString.Split(" ");
            var direction = Direction.N;
            switch (components[2])
            {
                case "N":
                    direction = Direction.N;
                    break;
                case "E":
                    direction = Direction.E;
                    break;
                case "S":
                    direction = Direction.S;
                    break;
                case "W":
                    direction = Direction.W;
                    break;
            }

            return new Position(int.Parse(components[0]), int.Parse(components[1]), direction);
        }
    }
}
