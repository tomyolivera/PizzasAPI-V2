namespace PizzasAPI.Models{
    public class Usuario{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string TokenExpiration { get; set; }
    }

}