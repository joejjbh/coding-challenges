using System;

namespace RobotWarsKata
{
    public class RobotWarsException : SystemException
    {
        public RobotWarsException() : base()
        {
        }

        public RobotWarsException(string message)
            : base(message)
        {
        }
    }
}