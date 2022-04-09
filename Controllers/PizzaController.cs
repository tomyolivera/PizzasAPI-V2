using Microsoft.AspNetCore.Mvc;
using PizzasAPI.Models;
using PizzasAPI.Helpers;
using PizzasAPI.Services;
using System.Collections.Generic;

namespace PizzasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll(){
            List<Pizza> ListaPizzas = PizzaService.GetAll();
            return Ok(ListaPizzas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            Pizza Pizza = PizzaService.GetById(id);

            if(Pizza == null)
            {
                return NotFound();
            }

            return Ok(Pizza);
        }

        [HttpPost]
        public IActionResult Create(Pizza Pizza){
            string headerToken = Request.Headers["token"];

            if(!Helper.IsValidToken(headerToken))
                return Unauthorized();

            PizzaService.Insert(Pizza);
            return CreatedAtAction(nameof(Create), new {id = Pizza.Id}, Pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza Pizza){
            string headerToken = Request.Headers["token"];
            
            if(!Helper.IsValidToken(headerToken))
                return Unauthorized();

            if(Pizza.Id != id)
                return BadRequest();

            if(PizzaService.GetById(id) == null)
                return NotFound();
            
            int affectedRows = PizzaService.Update(Pizza);

            if(affectedRows > 0)
                return Ok(Pizza);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id){
            string headerToken = Request.Headers["token"];
            
            if(!Helper.IsValidToken(headerToken))
                return Unauthorized();

            Pizza Pizza = PizzaService.GetById(id);

            if(Pizza == null)
                return NotFound();
            
            int affectedRows = PizzaService.Delete(id);

            if(affectedRows > 0)
                return Ok(Pizza);

            return NotFound();
        }
    }
}
