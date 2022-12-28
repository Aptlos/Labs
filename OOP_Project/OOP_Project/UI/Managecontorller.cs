using System;
using System.Collections.Generic;
using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class ManageContorller
    {
        public List<IUserInterface> UIs { get; set; }
        public static List<string> Types { get; set; }
        public static List<string> Goods { get; set; }
        public static Users User { get; set; }
        public ManageContorller()
        {
            User = new Users("Artem",123,DataWork.GetIdUser("Artem"));
            UIs = new List<IUserInterface>();
            UIs.Add(new ShowAllTypesController());
            UIs.Add(new ConfirmPurchaseController());
            UIs.Add(new AddToBalanceController());
            Types  = new List<string>(DataWork.GetTypes());
            Goods = new List<string>(DataWork.GetAllProducts());
        }

        public void Show()
        {
            int i = 1;
            Console.WriteLine("Main menu:");
            Console.WriteLine(" 0.Exit");
            foreach (var ui in UIs)
            {
                Console.WriteLine("{0,2}.{1,13}",i,ui.Message());
                i++;
            }
        }

        private void Action()
        {
            Console.WriteLine("Select action: ");
            int action = int.Parse(Console.ReadLine());
            var exit = new ExitController();
            if (action != 0)
            {
                if (action > UIs.Count || action<0)
                {
                    Console.WriteLine("There is now such function");
                    Action();
                }else
                UIs[action - 1].Action();
            }
            else exit.Action();
        }
        
        public void Run()
        {
            while (true)
            {
                try
                {
                    Show();
                    Action();
                }
                catch (Exception)
                {
                    break;
                }
            }
        }
    }
}