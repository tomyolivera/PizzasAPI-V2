using System;
using PizzasAPI.Models;
using PizzasAPI.Helpers;
using PizzasAPI.Utils;
using System.Data.SqlClient;
using Dapper;

namespace PizzasAPI.Services
{
    public static class UsuarioService
    {
        static SqlConnection connection = BD.GetConnection();

        public static Usuario Login(string userName, string password)
        {
            Usuario Usuario = GetByUserNamePassword(userName, password);

            if(Usuario != null)
                Usuario.Token = RefreshToken(Usuario.Id);

            return Usuario;
        }

        public static Usuario GetByUserNamePassword(string userName, string password)
        {
            Usuario Usuario = connection.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuarios WHERE userName = @userName AND password = @password",
                                                                        new {userName, password});

            return Usuario;
        }

        public static Usuario GetByToken(string token)
        {
            Usuario Usuario = connection.QueryFirstOrDefault<Usuario>("SELECT * FROM Usuarios WHERE token = @token", new {token});
            
            if(Usuario != null)
                if(Usuario.TokenExpirationDate < DateTime.Now)
                    Usuario = null;

            return Usuario;
        }

        private static string RefreshToken(int id)
        {
            DateTime tokenExpirationDate = DateTime.Now.AddMinutes(15);
            string token = Helper.GetToken();
            connection.Execute("UPDATE Usuarios SET token = @token, tokenExpirationDate = @tokenExpirationDate WHERE id = @id", new {token, tokenExpirationDate, id});
            return token;
        }
    }
}