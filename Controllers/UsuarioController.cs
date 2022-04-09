using Microsoft.AspNetCore.Mvc;
using PizzasAPI.Models;
using PizzasAPI.Services;

namespace PizzasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Usuario Usuario){
            Usuario UsuarioLogin = UsuarioService.Login(Usuario.UserName, Usuario.Password);

            if(UsuarioLogin == null)
                return NotFound();

            return Ok(UsuarioLogin.Token);
        }
    }
}
