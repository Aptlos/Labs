using System;
using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class DelFromBasketController : IUserInterface
    {
        public string Message()
        {
            return "Delete product from basket";
        }

        public void Action()
        {
            Console.WriteLine("Select the product which you want to delete: ");
            string act = Console.ReadLine();
            int action;
            if (!int.TryParse(act, out action))
            {
                Console.WriteLine("This is not a number");
                Action();
            }

            if (action != 0)
            {
                if (action < 0 && action > DataWork.CountBasket())
                {
                    Console.WriteLine("There is no such product in basket");
                    Action();
                }
                DataWork.DelFromBasket(action);
            }
            
        }
    }
}