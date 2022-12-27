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
            int ind = int.Parse(Console.ReadLine());
            if (ind != 0)
            {
                //var products = new List<string[]>(DataWork.GetProducts(ind));
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