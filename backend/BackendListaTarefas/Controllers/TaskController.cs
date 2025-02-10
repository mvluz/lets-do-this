using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Models;

namespace Backend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    // GET: api/tasks
    [HttpGet]
    public async Task<ActionResult<List<TaskShare>>> GetTasks()
    {
        return await _taskService.GetTasksAsync();
    }

    // GET: api/tasks/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskShare>> GetTask(string id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return task;
    }

    // POST: api/tasks
    [HttpPost]
    public async Task<ActionResult<TaskShare>> CreateTask(TaskShare task)
    {
        await _taskService.CreateTaskAsync(task);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    // PUT: api/tasks/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(string id, TaskShare task)
    {
        var existingTask = await _taskService.GetTaskByIdAsync(id);
        if (existingTask == null)
        {
            return NotFound();
        }

        await _taskService.UpdateTaskAsync(id, task);
        return NoContent();
    }

    // DELETE: api/tasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(string id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        await _taskService.DeleteTaskAsync(id);
        return NoContent();
    }
}
