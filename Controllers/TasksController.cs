using BrainsToDo.Models;
using BrainsToDo.DTOModels;
using BrainsToDo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task = BrainsToDo.Models.Task;

namespace BrainsToDo.Controllers
{
    [ApiController]
    [Route("Tasks")]
    [Authorize]
    public class TasksController(DataContext dataContext):ControllerBase
    {
        private readonly DataContext _context = dataContext;

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = _context.Task.ToList();
            if (tasks.Count == 0) return NotFound("No tasks found");
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = _context.Task.Find(id);
            if (task == null) return NotFound("Task not found");
            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask(TaskDTO task)
        {
            var tasks = _context.Task.ToList();
            var oldTask = tasks.Find(x => x.Name == task.Name);
            if (task == null) return BadRequest("Empty request");

            Task newTask = new Task()
            {
                Name = task.Name,
                Description = task.Description,
                createdAt = DateTime.UtcNow,
                updatedAT = DateTime.UtcNow,
            };

            _context.Task.Add(newTask);
            _context.SaveChanges();
            return Created($"http://localhost:5202/tasks/{newTask.Id}", newTask);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(Task task, int id)
        {
            var oldTask = _context.Task.Find(task.Id);
            
            if(oldTask == null) return NotFound("Task not found");
            
            _context.Task.Remove(oldTask);
            _context.Task.Add(task);
            _context.SaveChanges();
            return Created($"http://localhost:5202/tasks/{task.Id}", task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Task.Find(id);
            if(task == null) return NotFound ("Task not found");

            _context.Task.Remove(task);
            _context.SaveChanges();
            return Ok(task);
        }
        

    }
}