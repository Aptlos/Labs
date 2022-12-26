namespace OOP_Project.DataBase
{
    public struct Users
    {
        public string Name { get; set; }
        public int Pas { get; set; }
        
        public double Bal { get; set; }

        public Users(string name, int pas)
        {
            Name = name;
            Pas = pas;
            Bal = 0;
        }
    }
}