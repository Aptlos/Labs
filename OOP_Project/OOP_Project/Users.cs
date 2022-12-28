﻿namespace OOP_Project.DataBase
{
    public struct Users
    {
        public string Name { get; set; }
        public string Pas { get; set; }
        
        public double Bal { get; set; }
        public int Id { get; set; }

        public Users(string name, string pas,int id)
        {
            Name = name;
            Pas = pas;
            Bal = 0;
            Id = id;
        }
    }
}