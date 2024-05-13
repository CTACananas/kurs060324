using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs060324
{
    public class DB
    {
        public SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UCA27K4\SQLSERVER;Initial Catalog=Kofein;Integrated Security=True");

        // Метод открытия соединения с базой данных.
        public void openConnection()
        {
            // Проверяем, что соединение закрыто, и открываем его.
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }
        // Метод закрытия соединения с базой данных.
        public void closeConnection()
        {
            // Проверяем, что соединение открыто, и закрываем его.
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
        // Метод получения объекта соединения.
        public SqlConnection getConnection()
        {
            return con;
        }
    }
}
