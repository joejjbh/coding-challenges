using System;

namespace SupermarketKata
{
    public class SupermarketKataException : SystemException
    {
        public SupermarketKataException() : base()
        {
        }

        public SupermarketKataException(string message)
            : base(message)
        {
        }
    }
}