using BrainsToDo.Data;
using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Models;

[ApiController]
    [Route("user")]
    public class ProductController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet]
        public IActionResult GetAllProducts() 
        {
            var products = _context.Users.ToList();
            if(products.Count == 0) return NotFound("No users were found.");
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }
        [HttpPost]
        public IActionResult CreateProduct(User users) 
        {
            var user = _context.Users.ToList();
            var oldUser = user.Find(x => x.Name == users.Name);
            if (oldUser != null) return BadRequest("User with provided name already exists");

            User newUser = new User() { Name = users.Name };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Created($"http://localhost:5179/users/{newUser.Id}", newUser);
        }
        [HttpPut]
        public IActionResult UpdateProduct(User user)
        {
            var oldUser = _context.Users.Find(user.Id);

            if (oldUser == null) return NotFound("User not found");
            if (oldUser.Name.Equals(user.Name)) return BadRequest("User with the provided name already exists.");

            _context.Users.Remove(oldUser);
            _context.Users.Add(user);
            _context.SaveChanges();
            return Created($"http://localhost:5179/users/{user.Id}", user);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var user = _context.Users.Find(id);
            if (user is null) return NotFound("User not found.");

            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok(user);
        }

}