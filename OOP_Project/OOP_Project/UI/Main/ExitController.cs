using System;

namespace OOP_Project.UI
{
    public class ExitController : IUserInterface 
    {
        public string Message()
        {
            return "Повернутись";
        }

        public void Action()
        {
            throw new Exception();
        }
    }
}