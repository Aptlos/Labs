using System;
using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class BasketController : IUserInterface
    {
        public string Message()
        {
            return "Your Basket";
        }

        public void Action()
        {
            if(DataWork.SeeBasket())
            {
                Console.WriteLine(" -----------------");
                Console.WriteLine(" 0.Return");
                var control = new DelFromBasketController();
                Console.WriteLine(" -----------------");
                Console.WriteLine("Actions:");
                Console.WriteLine(" 1." + control.Message());
                Console.WriteLine(" 2.Clear Basket");
                Console.WriteLine(" -----------------");
                Console.WriteLine(" 0.Return"); 
                Console.WriteLine("Choose action: ");
                string act = Console.ReadLine();
                int action;
                if (!int.TryParse(act, out action))
                {
                    Console.WriteLine("This is not a number");
                    Action();
                }
                while (true)
                {
                    if (action != 0)
                    {
                        if (action == 1)
                        {
                            control.Action();
                            break;
                        }

                        if (action == 2)
                        {
                            DataWork.ClearBasket();
                            break;
                        }

                        Console.WriteLine("There is no such action");
                    }
                    break;
                }
            }
        }
    }
}