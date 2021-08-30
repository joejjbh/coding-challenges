namespace RobotWarsKata
{
    public class ArenaCommand
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public ArenaCommand(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}