using System;
using System.Collections.Generic;
using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class AddingController : IUserInterface
    {
        public string Message()
        {
            return "Choose a product:";
        }

        public void Action()
        {
            var exit = new ExitController();
            string act = Console.ReadLine();
            int ind;
            if (!int.TryParse(act, out ind))
            {
                Console.WriteLine("This is not a number");
                Console.WriteLine("Choose a product:");
                Action();
            }
            if (ind != 0)
            {
                if (ind > ProductsController.Products.Count || ind < 0)
                {
                    Console.WriteLine("There is no such product");
                    Console.WriteLine(Message());
                    Action();
                }
                else
                    DataWork.AddToBasket(ManageContorller.Goods.IndexOf(ProductsController.Products[ind-1][0])+1,1);
            }
            else
            {
                Console.WriteLine("Types of products:");
                Console.WriteLine(" 0.Return");
                int i = 1;
                foreach (var type in ManageContorller.Types)
                {
                    Console.WriteLine("{0,2}.{1,4}",i,type);
                    i++;
                }
                exit.Action();
            }
        }
    }
}