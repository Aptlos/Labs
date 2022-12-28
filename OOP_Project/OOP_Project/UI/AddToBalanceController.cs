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
            double bal = double.Parse(Console.ReadLine());
            DataWork.AddBal(ManageContorller.User.Id,bal);
        }
    }
}