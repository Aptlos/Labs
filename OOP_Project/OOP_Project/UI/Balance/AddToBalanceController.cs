using System;
using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class AddToBalanceController : IUserInterface
    {
        public string Message()
        {
            return "Add money to balance";
        }

        public void Action()
        {
            Console.WriteLine("Input amount of money:");
            string act = Console.ReadLine();
            double bal;
            if (!double.TryParse(act, out bal))
            {
                Console.WriteLine("This is not a number");
                Action();
            }

            if (bal < 0)
            {
                Console.WriteLine("You can't add amount of money <0");
                Action();
            }
            
            DataWork.AddBal(ManageContorller.User.Id,bal);
            DataWork.GetBal(ManageContorller.User.Id);
        }
    }
}