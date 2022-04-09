using System;
using System.Data.SqlClient;
using PizzasAPI.Models;
using PizzasAPI.Utils;
using PizzasAPI.Services;

namespace PizzasAPI.Helpers{
    public static class Helper{

        static SqlConnection connection = BD.GetConnection();

        public static string GetToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool IsValidToken(string token)
        {
            Usuario Usuario = UsuarioService.GetByToken(token);
            return Usuario != null;
        }
    }
}