using System;
using System.Collections.Generic;
using OOP_Project.DataBase;
using OOP_Project.UI;

namespace OOP_Project
{
    class Program
    {
        public static List<int> Basket;
        static void Main(string[] args)
        {
            DataWork.RunDB();
            Basket = new List<int>();
            var user = new Users("Artem",123);
            var controller = new ManageContorller();
            controller.Run();
        }
    }
}