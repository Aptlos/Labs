using OOP_Project.DataBase;

namespace OOP_Project.UI
{
    public class ShowBalController : IUserInterface
    {
        public string Message()
        {
            return "Check balance";
        }

        public void Action()
        {
            DataWork.GetBal(ManageContorller.User.Id);
        }
    }
}