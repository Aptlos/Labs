using System;
using System.Collections.Generic;
using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class ManageContorller
    {
        public List<IUserInterface> UIs { get; set; }
        
        //public List<IUserInterface> UILogins { get; set; }
        public static List<string> Types { get; set; }
        public static List<string> Goods { get; set; }
        public static Users User { get; set; }
        public StartController StartUi;
        public ManageContorller()
        {
            UIs = new List<IUserInterface>();
            /*UILogins = new List<IUserInterface>();
            UIs.Add(new RegistrationController());
            UIs.Add(new LogInController());*/
            UIs.Add(new ShowAllTypesController());
            UIs.Add(new BasketController());
            UIs.Add(new ConfirmPurchaseController());
            UIs.Add(new ShowBalController());
            UIs.Add(new AddToBalanceController());
            StartUi = new StartController();
            Types  = new List<string>(DataWork.GetTypes());
            Goods = new List<string>(DataWork.GetAllProducts());
        }

        public void Show()
        {
            int i = 1;
            Console.WriteLine("Main menu:");
            foreach (var ui in UIs)
            {
                Console.WriteLine("{0,2}.{1,3}",i,ui.Message());
                i++;
            }
            Console.WriteLine(" --------------------------");
            Console.WriteLine(" 0.Leave from account");
        }

        private void Action()
        {
            Console.WriteLine("Select action: ");
            string act = Console.ReadLine();
            int action;
            if (!int.TryParse(act, out action))
            {
                Console.WriteLine("This is not a number");
                Action();
            }
            var exit = new ExitController();
            if (action != 0)
            {
                if (action > UIs.Count || action<0)
                {
                    Console.WriteLine("There is no such function");
                    Action();
                }else  UIs[action - 1].Action();
            }
            else
            {
                DataWork.ClearBasket();
                exit.Action();
            }
        }
        
        public void Run()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(StartUi.Message());
                    StartUi.Action();
                }
                catch (Exception)
                {
                    break;
                }
            }
            while (true)
            {
                try
                {
                    Show();
                    Action();
                }
                catch (Exception)
                {
                    Run();
                }
            }
        }
    }
}