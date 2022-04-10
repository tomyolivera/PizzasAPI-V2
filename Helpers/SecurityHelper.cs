using System;
using PizzasAPI.Services;

namespace PizzasAPI.Helpers{
    public static class Helper{
        public static string GetToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool IsValidToken(string token)
        {
            return UsuarioService.GetByToken(token) != null;
        }
    }
}