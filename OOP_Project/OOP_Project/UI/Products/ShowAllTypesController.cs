using System;
using System.Collections.Generic;
using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class ShowAllTypesController : IUserInterface

    {
        public void Action()
        {
            Console.WriteLine("Types of products:");
            int i = 1;
            
            foreach (var type in ManageContorller.Types)
            {
                Console.WriteLine("{0,2}.{1,4}",i,type);
                i++;
            }

            Console.WriteLine(" -------------------");
            Console.WriteLine(" 0.Return");
            var control = new ProductsController();
            while (true)
            {
                try
                {

                    Console.WriteLine(control.Message());
                    control.Action();

                }
                catch (Exception)
                {
                    break;
                }
            }

        }

        public string Message()
        {
            return "Show all types of products";
        }
    }
}