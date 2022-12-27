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
        public ManageContorller()
        {
            UIs = new List<IUserInterface>();
            UIs.Add(new ShowAllTypesController());
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
            if(action!=0)
                UIs[action-1].Action();
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