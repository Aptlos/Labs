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
            Console.WriteLine(" 0.Exit");
            foreach (var ui in UILogins)
            {
                Console.WriteLine("{0,2}.{1,3}",i,ui.Message());
                i++;
            }
            int action = int.Parse(Console.ReadLine());
            if (action != 0)
            {
                UILogins[action-1].Action();
            }
            else exit.Action();
        }
    }
}