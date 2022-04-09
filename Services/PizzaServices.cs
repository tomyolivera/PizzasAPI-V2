using PizzasAPI.Models;
using PizzasAPI.Utils;
using System.Data.SqlClient;
using System.Collections.Generic;
using Dapper;

namespace PizzasAPI.Services
{
    public static class PizzaService
    {
        static SqlConnection connection = BD.GetConnection();

        public static List<Pizza> GetAll()
        {
            List<Pizza> ListaPizzas = new List<Pizza>();

            ListaPizzas = connection.Query<Pizza>("SELECT * FROM Pizzas").AsList();

            return ListaPizzas;
        }

        public static Pizza GetById(int id)
        {
            Pizza Pizza;

            Pizza = connection.QueryFirstOrDefault<Pizza>("SELECT * FROM Pizzas WHERE id = @id", new {id});

            return Pizza;
        }

        public static int Insert(Pizza Pizza)
        {
            string query = "INSERT INTO Pizzas VALUES (@nombre, @libreGluten, @importe, @descripcion) SELECT CAST(SCOPE_IDENTITY() AS INT)";

            int affectedRows = connection.Execute(query, new {
                                                        nombre = Pizza.Nombre,
                                                        libreGluten = Pizza.LibreGluten,
                                                        importe = Pizza.Importe,
                                                        descripcion = Pizza.Descripcion
                                                    });
            return affectedRows;
        }

        public static int Update(Pizza Pizza)
        {
            string query = "UPDATE Pizzas SET nombre = @nombre, libreGluten = @libreGluten, importe = @importe, descripcion = @descripcion WHERE id = @id";

            int affectedRows = connection.Execute(query, new {
                                                        id = Pizza.Id,
                                                        nombre = Pizza.Nombre,
                                                        libreGluten = Pizza.LibreGluten,
                                                        importe = Pizza.Importe,
                                                        descripcion = Pizza.Descripcion
                                                    });
            return affectedRows;
        }

        public static int Delete(int id)
        {
            int affectedRows = connection.Execute("DELETE FROM Pizzas WHERE id = @id", new {id});
            return affectedRows;
        }
    }
}