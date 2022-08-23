using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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
    
    [HttpPost("/getItems"),Authorize]
    public async Task<IActionResult> GetItems()
    {
        var usersItems = await _context.ToDoList.Where(n => n.UserId == Int32.Parse(User.Identity.Name)).ToListAsync();
        return Ok(usersItems);
    }
    
    [HttpPost("/Item")]
    public async Task<IActionResult> Post([FromBody] ToDoListClass reqBody)
    {
        reqBody.UserId = Int32.Parse(User.Identity.Name);
        _context.ToDoList.Add(reqBody);
        await _context.SaveChangesAsync();
        return Ok("ItemStored");
    }
    
    [HttpDelete("/Item"),Authorize]
    public async Task<IActionResult> Delete([FromBody] ToDoListClass reqBody)
    {
        var rowToDelete=await _context.ToDoList.FirstAsync( x => x.Id == reqBody.Id);
        _context.ToDoList.Remove(rowToDelete);
        await _context.SaveChangesAsync();
        return Ok("item is deleted");
    }
    
    [HttpPut,Authorize]
    public async Task<IActionResult> Put([FromBody] ToDoListClass reqBody)
    {
        reqBody.UserId = Int32.Parse(User.Identity.Name);
        var rowToUpdate = _context.ToDoList.Update(await _context.ToDoList.FirstAsync(x => x.Id == reqBody.Id)).Entity;
        rowToUpdate.ItemName = reqBody.ItemName;
        rowToUpdate.Discription = reqBody.Discription;
        await _context.SaveChangesAsync();
        return Ok("item is update");
    }
}

// [HttpGet]
// public IActionResult Search(int id)
// {
//     var UsersItem = _context.ToDoList.Where(n => n.UserId == id);
//     return Ok(UsersItem);
// }
//
// [HttpPost]
// public async Task<IActionResult> Post([FromBody] ToDoListClass reqBody)
// {
//     _context.ToDoList.Add(reqBody);
//     await _context.SaveChangesAsync();
//     return Ok("ItemStored");
// }
//
// [HttpDelete]
// public async Task<IActionResult> Delete([FromBody] ToDoListClass reqBody)
// {
//     var rowToDelete=await _context.ToDoList.FirstAsync( x => x.Id == reqBody.Id);
//     _context.ToDoList.Remove(rowToDelete);
//     await _context.SaveChangesAsync();
//     return Ok("item is deleted");
// }
//
// [HttpPut]
// public async Task<IActionResult> Put([FromBody] ToDoListClass reqBody)
// {
//     var rowToUpdate = _context.ToDoList.Update(await _context.ToDoList.FirstAsync( x => x.Id == reqBody.Id));
//     rowToUpdate.Entity.ItemName = reqBody.ItemName;
//     rowToUpdate.Entity.ItemName = reqBody.ItemName;
//     await _context.SaveChangesAsync();
//     return Ok("item is update");
// }