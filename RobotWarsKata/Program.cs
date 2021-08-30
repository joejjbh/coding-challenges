using System;
using System.IO;
using System.Reflection;

namespace RobotWarsKata
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = System.IO.File.ReadAllText("..\\netcoreApp3.1\\Inputs.txt");
            Console.WriteLine(text);
            Console.WriteLine();
            Console.WriteLine();

            var inputParser = new InputParser();
            var gameCommand = inputParser.ParseGameCommands(text);
            var arena = new Arena(gameCommand.Arena.Height, gameCommand.Arena.Width);

            foreach (var robot in gameCommand.Robots)
            {
                arena.Robots.Add(new Robot(robot.StartingPosition.XPosition, robot.StartingPosition.YPosition, robot.StartingPosition.Direction));
            }

            foreach (var robotCommand in gameCommand.Robots)
            {
                arena.MoveRobot(robotCommand);
            }

            var endPositions = arena.GetRobotPositions();

            foreach (var endPosition in endPositions)
            {
                Console.WriteLine($"{endPosition}");
            }

        }
    }
}
