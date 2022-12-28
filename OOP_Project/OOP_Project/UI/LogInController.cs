using System;
using System.Data;
using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class LogInController : IUserInterface
    {
        public string Message()
        {
            return "Log In";
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

                int id = DataWork.GetIdUser(login);
                if (id == 0)
                {
                    Console.WriteLine("There is no such user");
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

                if (DataWork.GetPasUser(id).Equals(pas))
                {
                    ManageContorller.User = new Users(login, pas, id);
                    exit.Action();
                    break;
                }
                Console.WriteLine("Password is wrong");
            }
        }
    }
}