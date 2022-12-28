using System;
using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class RegistrationController : IUserInterface
    {
        public string Message()
        {
            return "Registration";
        }

        public void Action()
        {
            var exit = new ExitController();
            while (true)
            {
                Console.WriteLine("Enter login:");
                string login = Console.ReadLine();
                if (login.Equals(""))
                {
                    Console.WriteLine("Login can't be empty");
                    continue;
                }
                
                if (login.Contains(" ")) 
                {
                    Console.WriteLine("Login can't have gaps");
                    continue;
                }
                    
                Console.WriteLine("Enter password:");
                string pas = Console.ReadLine();
                if (pas.Equals(""))
                {
                    Console.WriteLine("Password can't be empty");
                    continue;
                }
                if (pas.Contains(" ")) 
                {
                    Console.WriteLine("Password can't have gaps");
                    continue;
                }
                ManageContorller.User = new Users(login, pas, DataWork.RegUser(login, pas));
                exit.Action();
                break;
            }

        }
    }
}