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
            DataWork.ConfirmPurchase(ManageContorller.User.Id);
        }
    }
}