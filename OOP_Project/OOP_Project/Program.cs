using System;
using System.Collections.Generic;
using OOP_Project.DataBase;

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
            DataWork.AddToBasket(1, 1);
            DataWork.AddToBasket(2,2);
            DataWork.AddToBasket(3,3);
            DataWork.AddToBasket(4,4);
            DataWork.ConfirmPurchase(1);
            
        }
    }
}