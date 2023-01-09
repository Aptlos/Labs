using System;
using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class ConfirmPurchaseController : IUserInterface
    {
        public string Message()
        {
            return "Confirm Purchase";
        }

        public void Action()
        {
            if (DataWork.SeeBasket())
            {
                Console.WriteLine("------------------");
                Console.WriteLine("1.Confirm purchase");
                Console.WriteLine("------------------");
                Console.WriteLine("0.Return");
                Console.WriteLine("Select action: ");
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
                            DataWork.ConfirmPurchase(ManageContorller.User.Id);
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