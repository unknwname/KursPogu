using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlTypes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.Remoting.Messaging;

namespace KursPogud
{
    internal class Logic
    {
        private string connectionString = "Data Source=Menu.db;";
        public List<String[]> GetAllDishes(String filter)
        {
            List<String[]> dishes = new List<String[]>();


            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"SELECT * FROM MainMenu {filter}";
            // Создание объекта SQLiteCommand
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                // Выполнение запроса
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Обработка результата запроса
                    while (reader.Read())
                    {
                        String[] s = new string[7];
                        s[0] = reader.GetInt32(0).ToString();
                        s[1] = reader.GetString(1);
                        s[2] = reader.GetString(2);
                        s[3] = reader.GetInt32(3).ToString();
                        s[4] = reader.GetInt32(4).ToString(); ;
                        s[5] = reader.GetInt32(5).ToString();
                        s[6] = reader.GetInt32(6).ToString(); ;
                        dishes.Add(s);

                    }
                }
            }
            connection.Close();
            return dishes;
        }
        public String[] GetOrderDishes(int id)
        {
            String[] s = new string[7];


            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"SELECT * FROM MainMenu Where id={id}";
            // Создание объекта SQLiteCommand
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                // Выполнение запроса
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Обработка результата запроса
                    while (reader.Read())
                    {

                        s[0] = reader.GetInt32(0).ToString();
                        s[1] = reader.GetString(1);
                        s[2] = reader.GetString(2);
                        s[3] = reader.GetInt32(3).ToString();
                        s[4] = reader.GetInt32(4).ToString(); ;
                        s[5] = reader.GetInt32(5).ToString();
                        s[6] = reader.GetInt32(6).ToString(); ;


                    }
                }
            }
            connection.Close();
            return s;
        }
        public int GetLastIdOrder()
        {
            int id = 0;
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"SELECT MAX(id) FROM orders";
            // Создание объекта SQLiteCommand
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                // Выполнение запроса
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Обработка результата запроса
                    while (reader.Read())
                    {

                        id = reader.GetInt32(0);


                    }
                }
            }
            connection.Close();
            return id;
        }
        public void CreateNewOrder(int id)
        {
            
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"INSERT INTO orders (id,status) VALUES ({id},0)";
            // Создание объекта SQLiteCommand
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
        public void CreateNewOrderDishes(int id,int idOrder,int counter)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"INSERT INTO status_dishes (id_dishes,status,id_order,counter) VALUES ({id},0,{idOrder},{counter})";
            // Создание объекта SQLiteCommand
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public List<String[]> GetOrders()
        {
            List<String[]> orders = new List<String[]>();


            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"SELECT * FROM orders Where status <> 3";
            // Создание объекта SQLiteCommand
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                // Выполнение запроса
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Обработка результата запроса
                    while (reader.Read())
                    {
                        String[] s = new string[2];
                        s[0] = reader.GetInt32(0).ToString();
                        s[1] = reader.GetInt32(1).ToString();
                        orders.Add(s);

                    }
                }
            }
            connection.Close();
            return orders;
        }
        public List<String[]> GetDishesFromOrder(int id)
        {
            List<String[]> dishes = new List<String[]>();

            

            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"SELECT * FROM status_dishes where id_order={id}";
            // Создание объекта SQLiteCommand
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                // Выполнение запроса
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Обработка результата запроса
                    while (reader.Read())
                    {
                        String[] s = new string[9];
                        s[0] = reader.GetInt32(0).ToString();
                        string sql1 = $"SELECT * FROM MainMenu where id = {reader.GetInt32(0)}";
                        // Создание объекта SQLiteCommand
                        using (SQLiteCommand command1 = new SQLiteCommand(sql1, connection))
                        {
                            // Выполнение запроса
                            using (SQLiteDataReader reader1 = command1.ExecuteReader())
                            {
                                // Обработка результата запроса
                                while (reader1.Read())
                                {
                                    s[1] = reader1.GetString(1);
                                    s[2] = reader1.GetString(2);
                                    s[3] = reader1.GetInt32(3).ToString();
                                    s[4] = reader1.GetInt32(4).ToString();
                                    s[5] = reader1.GetInt32(5).ToString();
                                    s[6] = reader1.GetInt32(6).ToString();
                                   

                                }
                            }
                        }
                        s[7] = reader.GetInt32(1).ToString();
                        


                        dishes.Add(s);

                    }
                }
            }
            connection.Close();
            return dishes;
        }
        public void ChangeStatusOrder(int id,int status)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"UPDATE orders SET status = {status} WHERE id ={id}";
            // Создание объекта SQLiteCommand
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public void ChangeStatusDishes(int idOrder,int idDishes)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"UPDATE status_dishes SET status = 2 WHERE id_dishes ={idDishes} AND id_order={idOrder} AND status<2 LIMIT 1 ";
            // Создание объекта SQLiteCommand
            int rowCount;
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
        public void ChangeStatusDishesEaten(int idOrder, int idDish, int counter)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"UPDATE status_dishes SET status = 3 WHERE id_dishes ={idDish} AND id_order={idOrder} AND counter ={counter}";
            // Создание объекта SQLiteCommand
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
        public void ChangeStatusDishesAll(int idOrder, int idStatus, string filter)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"UPDATE status_dishes SET status = {idStatus} WHERE id_order={idOrder} {filter}";
            // Создание объекта SQLiteCommand
            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();

        }
        public int Logging(String login, String password, String status)
        {
            bool isCorrect = false;
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql = $"SELECT * FROM logging WHERE login='{login}' AND status ='{status}' ";

            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {

                // Выполнение запроса

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (password == reader.GetString(1)) isCorrect = true;

                    }
                    else
                    {
                        connection.Close();
                        return 1;
                    }
                }


            }
            connection.Close();
            if (isCorrect) return 0;
            else return 2;
        }

    }
}
