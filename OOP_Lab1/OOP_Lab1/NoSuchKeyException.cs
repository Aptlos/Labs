using System;

namespace OOP_Lab1
{
    public class NoSuchKeyException : Exception
    {
        public NoSuchKeyException(string message) : base(message){}
    }
}