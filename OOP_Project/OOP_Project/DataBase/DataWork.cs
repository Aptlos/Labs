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
            command.Parameters.Add("@par1", (DbType)SqlDbType.VarChar).Value = username;
            command.Parameters.Add("@par2", (DbType)SqlDbType.Binary).Value = password;
            command.Parameters.Add("@par3", (DbType)SqlDbType.Int).Value = bal;
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
            double cost = 0;
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
            double cost = 0;
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

        public static void GetBal(int id)
        {

            string sqlExpression = "SELECT Bal FROM Users WHERE Id=" + id;
            var command = new SQLiteCommand(sqlExpression, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    double s = reader.GetDouble(0);
                    Console.WriteLine("Your balance is {0,1}", s);
                }
            }
        }

        public static void AddToBasket(int pr)
        {
            
            int id = CountBasket()+1;
            string sqlExpression = "INSERT INTO Basket(Id,Name) VALUES ('" + id + "','" + pr + "')";
            var command = new SQLiteCommand(sqlExpression, Connection);
            command.ExecuteNonQuery();
        }

        public static void DelFromBasket(int id)
        {
            string sqlExpression = "DELETE FROM Basket WHERE Id=" + id;
            var command = new SQLiteCommand(sqlExpression, Connection);
            command.ExecuteNonQuery();
        }

        public static int CountBasket()
        {
            int count = 0;
            string sqlExpression = "SELECT MAX(Id) FROM Basket";
            var command = new SQLiteCommand(sqlExpression, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            object c=DBNull.Value;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    c = reader.GetValue(0);
                    if (c != DBNull.Value)
                    {
                        count = Convert.ToInt32(c); 
                    }
                }
            }

            return count;
        }

        public static bool SeeBasket()
        {
            bool result = false;
            string sqlExpression = "SELECT G.Name,G.Cost,B.Id FROM Basket B " +
                                   "INNER JOIN Goods G ON B.Name=G.Id";
            var command = new SQLiteCommand(sqlExpression, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine("Your basket:");
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    double cost = reader.GetDouble(1);
                    int id = reader.GetInt32(2);
                    Console.WriteLine(" {0,1}.{1,1} : {2,1}", id, name, cost);
                }

                result = true;
            }
            else Console.WriteLine("There is no products in your basket");

            return result;
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
                    string sqlExpression3 = "INSERT INTO Purch(User,Date,Cost) VALUES ('" + us + "','" + date + "','" + cost + "')";

                    LessBall(us, cost);
                     var command3 = new SQLiteCommand(sqlExpression3, Connection);
                    command3.ExecuteNonQuery();
                    command3.Cancel();

                    sqlExpression3 = "SELECT MAX(Id) FROM  Purch";
                    int id = 0;
                    command3.CommandText = sqlExpression3;
                    command3 = new SQLiteCommand(sqlExpression3, Connection);
                    SQLiteDataReader reader3 = command3.ExecuteReader();
                    if (reader3.HasRows)
                    {
                        while (reader3.Read())
                         {
                            id = reader3.GetInt32(0);
                         }
                    }

                    reader3.Close();
                    sqlExpression3 = "SELECT Name FROM Basket";
                    command3.CommandText = sqlExpression3;
                    command3 = new SQLiteCommand(sqlExpression3, Connection);
                    reader3 = command3.ExecuteReader();
                    if (reader3.HasRows)
                    {
                        while (reader3.Read())
                        {
                             int s = reader3.GetInt32(0);
                             string sqlExpression1 = "INSERT INTO Purch_list(Id,User,Name) VALUES ('" + id + "','" + us +
                                                "','" + s + "')";
                             var command1 = new SQLiteCommand(sqlExpression1, Connection);
                             command1.ExecuteNonQuery();
                        }
                     }

                    
                    reader3.Close();
                }
                else Console.WriteLine("You have not enough money.");
            }

            command.Cancel();
            reader.Close();
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
            byte[] pas = null;
            string sqlExpression = "SELECT \"rowid\",Pas FROM Users WHERE Id=" + id;
            var command = new SQLiteCommand(sqlExpression, Connection);
            SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var blob = reader.GetBlob(1, readOnly: true);
                    pas = new byte[blob.GetCount()];
                    reader.GetBytes(1, 0, pas, 0, blob.GetCount());
                }
            }

            return pas;
        }
    }
}

        
        
    
