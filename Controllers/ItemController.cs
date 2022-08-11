using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Domain;
using System.Linq;
using System.Collections.Generic;

namespace ToDoList.Controllers;

[ApiController]
[Route("/[Controller]")]
public class ItemController:ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public ItemController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Search(int id)
    {
        var UsersItem = _context.ToDoList.Where(n => n.UserId == id);
        return Ok(UsersItem);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ToDoListClass reqBody)
    {
        _context.ToDoList.Add(reqBody);
        await _context.SaveChangesAsync();
        return Ok("ItemStored");
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] ToDoListClass reqBody)
    {
        var RowToDelete=_context.ToDoList.FirstOrDefault( x => x.Id == reqBody.Id);
        _context.ToDoList.Remove(RowToDelete);
        return Ok("item is deleted");
    }
}