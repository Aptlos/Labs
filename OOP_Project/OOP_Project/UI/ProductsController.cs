﻿using System;
using System.Collections.Generic;
using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class ProductsController : IUserInterface
    {
        public static List<string[]> Products { get; set; }

        public string Message()
        {
            return "Select a type:";
        }

        public void Action()
        {
            var exit = new ExitController();
            int ind = int.Parse(Console.ReadLine());
            if (ind != 0)
            {
                int i = 1;
                Products = new List<string[]>(DataWork.GetProducts(ind));
                Console.WriteLine("Products of this type:");
                Console.WriteLine(" 0.Return");
                foreach (var prod in Products)
                {
                    Console.WriteLine("{0,2}.{1,3} Cost:{2,3}", i, prod[0], prod[1]);
                    i++;
                }
            }
            else
            {
                
                exit.Action();
            }
            var control = new AddingController();
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
    }
}