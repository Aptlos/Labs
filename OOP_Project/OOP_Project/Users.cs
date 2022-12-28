namespace OOP_Project.DataBase
{
    public struct Users
    {
        public string Name { get; set; }
        public int Pas { get; set; }
        
        public double Bal { get; set; }
        public int Id { get; set; }

        public Users(string name, int pas,int id)
        {
            Name = name;
            Pas = pas;
            Bal = 0;
            Id = id;
        }
    }
}