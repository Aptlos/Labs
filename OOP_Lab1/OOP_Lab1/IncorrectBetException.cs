using System;

namespace OOP_Lab1
{
    public class IncorrectBetException : Exception
    {
        public IncorrectBetException(string message) : base(message){}
    }
}