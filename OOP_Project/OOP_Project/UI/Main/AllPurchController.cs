using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class AllPurchController : IUserInterface
    {
        public string Message()
        {
            return "Show all purchases";
        }

        public void Action()
        {
            DataWork.ShowAllPurch(ManageContorller.User.Id);
        }
    }
}