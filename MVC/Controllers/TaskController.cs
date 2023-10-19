using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Service;

namespace MVC.Controllers;

public class TaskController : Controller
{
    private readonly TasksService _todoListService;

    public TaskController(TasksService todoListService)
    {
        _todoListService = todoListService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var tasks = await _todoListService.GetAsync();
        return View(tasks);
    }

    [HttpGet]
    public async Task<ActionResult<TaskEntity>> Get(string id)
    {
        var card = await _todoListService.GetAsync(id);

        if (card is null)
        {
        return NotFound();
        }

        return card;
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(string id)
    {
        var task = await _todoListService.GetAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask(TaskEntity newTask)
    {
        if (ModelState.IsValid)
        {
            await _todoListService.CreateAsync(newTask);
            return RedirectToAction("Index");
        }

        return View("Index", await _todoListService.GetAsync());
    }

    [HttpPut]
    public async Task<IActionResult> EditTask(string id, [FromBody] TaskEntity updatedTask)
    {
        var task = await _todoListService.GetAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        task.TaskName = updatedTask.TaskName;
        task.StartDate = updatedTask.StartDate;
        task.EndDate = updatedTask.EndDate;

        await _todoListService.UpdateAsync(id, task);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var task = await _todoListService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        await _todoListService.RemoveAsync(id);

        return Ok();
    }
}