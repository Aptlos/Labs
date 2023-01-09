using System;
using System.Security.Cryptography;
using System.Text;
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
                Console.WriteLine("Enter login:"); //Сделать проверку на то, есть ли уже такой юзер
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

                if (DataWork.GetIdUser(login)!=0)
                {
                    Console.WriteLine("There is user with such login");
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

                byte[] pasSource;
                byte[] pasHash;
                pasSource = ASCIIEncoding.ASCII.GetBytes(pas);
                pasHash = new MD5CryptoServiceProvider().ComputeHash(pasSource);
                ManageContorller.User = new Users(login, pasHash, DataWork.RegUser(login, pasHash));
                exit.Action();
                break;
            }

        }
    }
}