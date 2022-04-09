using System.Data.SqlClient;

namespace PizzasAPI.Utils{
    public static class BD {
        private static string CONNECTION_STRING = @"Server=DESKTOP-68PON6U\SQLEXPRESS;Database=DAI-Pizzas;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(CONNECTION_STRING);
            return connection;
        }
    }
}