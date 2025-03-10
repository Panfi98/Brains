using BrainsToDo.Models;
using BrainsToDo.DTOModels;
using BrainsToDo.Data;
using Microsoft.AspNetCore.Mvc;

namespace BrainsToDo.Controllers
{
    [ApiController]
    [Route("Tasks")]
    public class TasksController
    {
        private readonly TasksContext _context = context;

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = _context.Tasks.ToList();
            if (tasks.Count == 0) return NotFound("No tasks found");
            return Ok(tasts);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var tasks = _context.Tasks.Find(id);
            if (!task) return NotFound("Task not found");
            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask(ViewTasks task)
        {
            var tasks = _context.Tasks.ToList();
            var oldTask = tasks.Find(x => x.name == task.name);
            if (task) return BadRequest("Task already exists");

            Tasks newTask = new Tasks()
            {
                name = task.name,
                description = task.description,
                createdAt = DateTime.Now,
            };

            _context.Tasks.Add(newTask);
            _context.SaveChanges();
            return Created($"http://localhost:5202/tasks/{newTask.Id}", newTask);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(Tasks task, int id)
        {
            var oldTask = _context.Tasks.Find(task.Id);
            
            if(!oldTask) return NotFound("Task not found");
            
            _context.Tasks.Remove(oldTask);
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return Created($"http://localhost:5202/tasks/{task.Id}", task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if(!task) return NotFound ("Task not found");

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok(task);
        }
        

    }
}