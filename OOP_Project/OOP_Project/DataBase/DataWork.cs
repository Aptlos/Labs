using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Globalization;
using System.Reflection.Metadata;

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

        public static int RegUser(string username, byte[] password)
        {
            int id = 0;
            double bal = 0;
            string sqlExpression = "INSERT INTO Users (Name, Pas,Bal) VALUES (@par1,@par2,@par3)";
            var command = new SQLiteCommand(sqlExpression, Connection);
            command.Parameters.Add("@par1", (DbType)SqlDbType.VarChar).Value=username;
            command.Parameters.Add("@par2",(DbType)SqlDbType.Binary).Value=password;
            command.Parameters.Add("@par3", (DbType)SqlDbType.Int).Value=bal;
            command.Prepare();
            command.ExecuteNonQuery();
            sqlExpression = "SELECT MAX(Id) FROM Users";
            command.CommandText = sqlExpression;
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
            }
            return id;
        }

        public static void AddBal(int id, double bal)
        {
            double cost=0;
            string sqlExpression = "SELECT Bal FROM Users WHERE Id=" + id;
            var command = new SQLiteCommand(sqlExpression, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    double s = reader.GetDouble(0);
                    cost += s;
                }

                cost += bal;
            }
            reader.Close();
            sqlExpression = "UPDATE Users SET Bal=" + cost + " WHERE Id=" + id;
            command.CommandText = sqlExpression;
            command.ExecuteNonQuery();
        }

        public static void LessBall(int id, double bal)
        {
            double cost=0;
            string sqlExpression = "SELECT Bal FROM Users WHERE Id=" + id;
            var command = new SQLiteCommand(sqlExpression, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    double s = reader.GetDouble(0);
                    cost += s;
                }

                cost -= bal;
            }
            reader.Close();
            sqlExpression = "UPDATE Users SET Bal=" + cost + " WHERE Id=" + id;
            command.CommandText = sqlExpression;
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
            
            string sqlExpression2 = "SELECT Bal FROM Users WHERE Id=" + us;
            var command2 = new SQLiteCommand(sqlExpression2, Connection);
            SQLiteDataReader reader2 = command2.ExecuteReader();
            double balance = 0;
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    balance = reader2.GetDouble(0);
                }
                
            }

            if (balance >= cost)
            {
                sqlExpression = "INSERT INTO Purch(User,Date,Cost) VALUES ('" + us + "','" + date + "','" + cost + "')";

                LessBall(us, cost);
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
                        string sqlExpression1 = "INSERT INTO Purch_list(Id,User,Name) VALUES ('" + id + "','" + us +
                                                "','" + s + "')";
                        var command1 = new SQLiteCommand(sqlExpression1, Connection);
                        command1.ExecuteNonQuery();
                    }
                }

                reader.Close();
            }
            else Console.WriteLine("You have not enough money. Make a ne purchase");
            ClearBasket();
        }

        public static List<string> GetTypes()
        { 
            var Types = new List<string>(); 
            string sqlExpression = "SELECT Name FROM Types"; 
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

        public static List<string[]> GetProducts(int i)
        {
            var row = new string[2];
            var Goods = new List<string[]>(); 
            string sqlExpression = "SELECT Name,Cost FROM Goods WHERE Type =" + i; 
            var command = new SQLiteCommand(sqlExpression, Connection); 
            SQLiteDataReader reader = command.ExecuteReader(); 
            if (reader.HasRows) 
            {
                while (reader.Read())
                {
                    row = new string[2];
                    row[0] = reader.GetString(0);
                    row[1] = reader.GetDouble(1).ToString();
                    Goods.Add(row);
                }
            }
            return Goods;
        }
        
        public static List<string> GetAllProducts()
        {
            var Goods = new List<string>(); 
            string sqlExpression = "SELECT Name FROM Goods"; 
            var command = new SQLiteCommand(sqlExpression, Connection); 
            SQLiteDataReader reader = command.ExecuteReader(); 
            if (reader.HasRows) 
            {
                while (reader.Read())
                {
                    string prod = reader.GetString(0);
                    Goods.Add(prod);
                }
            }
            return Goods;
        }
        
        public static int GetIdUser(string name)
        {
            int id = 0;
            string sqlExpression = "SELECT Id FROM Users WHERE Name=" + "'" + name + "'";
            var command = new SQLiteCommand(sqlExpression, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
            }

            return id;
        }

        public static byte[] GetPasUser(int id)
        {
            byte[] pas=null;
            string sqlExpression = "SELECT \"rowid\",Pas FROM Users WHERE Id=" + id;
            var command = new SQLiteCommand(sqlExpression, Connection);
            SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var blob = reader.GetBlob(1,readOnly:true);
                    pas = new byte[blob.GetCount()];
                    reader.GetBytes(1,0,pas,0,blob.GetCount());
                }
            }

            return pas;
        }
    }
}