using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Globalization;

namespace OOP_Project.DataBase
{
    internal class DataWork
    {
        private static SQLiteConnection Connection;

        public static void RunDB()
        {
            //string connectionString = @"Data Source=C:\Users\Артём\RiderProjects\ConsoleApplication1\ConsoleApplication1\Rewards;Version=3";
            string path = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "Shop";
            string connectionString = @"Data Source=" + path + ";Version=3";

            Connection = new SQLiteConnection(connectionString);
            try
            {
                Connection.Open();
                //Console.WriteLine("Підключення відкрито");
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void StopDB()
        {
            Connection.Close();
        }

        public static void ClearBasket()
        {
            string sqlExpression = "DELETE FROM Basket";
            var command = new SQLiteCommand(sqlExpression, Connection);
            command.ExecuteNonQuery();
        }

        public static void RegUser(Users user)
        {
            int id = 0;
            string name = user.Name;
            int pas = user.Pas;
            double bal = user.Bal;

            string sqlExpression = "INSERT INTO Users (Name, Pas,Bal) VALUES (" + "'" + name + "','" + pas+ "','" + bal + "')";
            var command = new SQLiteCommand(sqlExpression, Connection);
            command.ExecuteNonQuery();
        }

        public static void AddToBasket(int pr,int id)
        {
            string sqlExpression = "INSERT INTO Basket(Id,Name) VALUES ('" + id + "','" + pr + "')";
            var command = new SQLiteCommand(sqlExpression, Connection);
            command.ExecuteNonQuery();
        }

        public static void ConfirmPurchase(int us)
        {
            DateTime localDate = DateTime.Now;
            string date = localDate.ToString();
            double cost = 0;
            string sqlExpression = "SELECT G.Cost FROM Goods G" +
                                   " INNER JOIN Basket B ON B.Name=G.Id";
            var command = new SQLiteCommand(sqlExpression, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    double s = reader.GetDouble(0);
                    cost += s;
                }
            }
            command.Cancel();
            reader.Close();
            sqlExpression = "INSERT INTO Purch(User,Date,Cost) VALUES ('" + us + "','" + date + "','" + cost + "')";
            command.CommandText = sqlExpression;
            command.ExecuteNonQuery();
            command.Cancel();
            
            sqlExpression = "SELECT MAX(Id) FROM  Purch";
            int id = 0;
            command.CommandText = sqlExpression;
            command = new SQLiteCommand(sqlExpression, Connection);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
            }
            reader.Close();
            sqlExpression = "SELECT Name FROM Basket";
            command.CommandText = sqlExpression;
            command = new SQLiteCommand(sqlExpression, Connection);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int s = reader.GetInt32(0);
                    string sqlExpression1="INSERT INTO Purch_list(Id,User,Name) VALUES ('" + id + "','" + us + "','" + s + "')";
                    var command1 = new SQLiteCommand(sqlExpression1, Connection);
                    command1.ExecuteNonQuery();
                }
            }
            reader.Close();
            ClearBasket();
        }

        public static List<string> GetTypes()
        { 
            var Types = new List<string>(); 
            string sqlExpression = "SELECT Name FROM Basket"; 
            var command = new SQLiteCommand(sqlExpression, Connection); 
            SQLiteDataReader reader = command.ExecuteReader(); 
            if (reader.HasRows) 
            {
               while (reader.Read())
               {
                   string t = reader.GetString(0);
                   Types.Add(t);
               }
           }
            return Types;
        }

    }
}