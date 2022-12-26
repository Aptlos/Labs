namespace OOP_Project.DataBase
{
    public struct Goods
    {
        public int Name { get; set; }
        public double Bal { get; set; }

        public Goods(int name, int pas)
        {
            Name = name;
            Bal = 0;
        }
    }
}