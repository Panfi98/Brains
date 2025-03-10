using BrainsToDo.DTOModels;
using BrainsToDo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BrainsToDo.Models;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public UserController()
    {
    }

    [HttpGet]
    public IActionResult Get(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IStatusCodeActionResult Creat(User user)
    {
        return Ok();
    }

    [HttpPut(template: "{id}")]
    public IStatusCodeActionResult Update(int id, User user)
    {
        return Ok();
    }

    [HttpDelete(template: "{id}")]
    public IStatusCodeActionResult Delete(int id)
    {
        return Ok();
    }

}