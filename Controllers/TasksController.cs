using BrainsToDo.Models;
using BrainsToDo.DTOModels;
using BrainsToDo.Data;
using Microsoft.AspNetCore.Mvc;

namespace BrainsToDo.Controllers
{
    [ApiController]
    [Route("Tasks")]
    public class TasksController(DataContext dataContext):ControllerBase
    {
        private readonly DataContext _context = dataContext;

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = _context.Tasks.ToList();
            if (tasks.Count == 0) return NotFound("No tasks found");
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound("Task not found");
            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask(TasksDTO task)
        {
            var tasks = _context.Tasks.ToList();
            var oldTask = tasks.Find(x => x.name == task.name);
            if (task == null) return BadRequest("Empty request");

            Tasks newTask = new Tasks()
            {
                name = task.name,
                description = task.description,
                createdAt = DateTime.UtcNow,
                updatedAT = DateTime.UtcNow,
            };

            _context.Tasks.Add(newTask);
            _context.SaveChanges();
            return Created($"http://localhost:5202/tasks/{newTask.Id}", newTask);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(Tasks task, int id)
        {
            var oldTask = _context.Tasks.Find(task.Id);
            
            if(oldTask == null) return NotFound("Task not found");
            
            _context.Tasks.Remove(oldTask);
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return Created($"http://localhost:5202/tasks/{task.Id}", task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if(task == null) return NotFound ("Task not found");

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok(task);
        }
        

    }
}