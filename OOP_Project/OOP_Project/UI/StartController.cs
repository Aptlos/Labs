using System;
using System.Collections.Generic;

namespace OOP_Project.UI
{
    public class StartController : IUserInterface
    {
        public List<IUserInterface> UILogins { get; set; }

        public StartController()
        {
            UILogins = new List<IUserInterface>();
            UILogins.Add(new LogInController());
            UILogins.Add(new RegistrationController());
        }
        public string Message()
        {
            return "Enter the shop:";
        }

        public void Action()
        {
            int i = 1;
            var exit = new ExitController();
            
            foreach (var ui in UILogins)
            {
                Console.WriteLine("{0,2}.{1,3}",i,ui.Message());
                i++;
            }

            Console.WriteLine(" --------------");
            Console.WriteLine(" 0.Exit");
            Console.WriteLine("Choose action:");
            
            
            string act = Console.ReadLine();
            int action;
            if (!int.TryParse(act, out action))
            {
                Console.WriteLine("This is not a number");
                Action();
            }
            
            if (action != 0)
            {
                if (action > UILogins.Count || action < 0)
                {
                    Console.WriteLine("There is no such function");
                    Action();
                }
                UILogins[action-1].Action();
            }
            Environment.Exit(0);
            
        }
    }
}