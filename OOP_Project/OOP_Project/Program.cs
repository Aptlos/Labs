using System;
using OOP_Project.DataBase;
using OOP_Project.UI;

namespace OOP_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DataWork.RunDB();
            var controller = new ManageContorller();
            controller.Run();
            DataWork.StopDB();
        }
    }
}