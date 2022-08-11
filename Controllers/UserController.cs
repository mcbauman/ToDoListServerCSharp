using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Domain;

namespace ToDoList.Controllers;
[ApiController]
[Route("/[Controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Users);
    }

    // GET login?name=name&password=password
    [HttpGet("/login")]
    public IActionResult Get([FromQuery] loginReqClass reqQuery)
    {
        return Ok(reqQuery);
    }
    
    [HttpGet("/signin")]
    public IActionResult Search(string name,string password )
    {
        try
        {
            UserClass currentUser = _context.Users.FirstOrDefault(x => x.Name == name);
            if (currentUser.Password == password)
            {
                Console.WriteLine("Incomming Request");
                return Ok(currentUser.Id);
            }
            return Ok("Password missmatch");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

//ID should be created by the Backend, also remove id in FrontEnd/CreateUser Line 14!
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] UserClass reqBody)
    {
        _context.Users.Add(reqBody);
        await _context.SaveChangesAsync();
        UserClass currentUser = _context.Users.FirstOrDefault(x => x.Name == reqBody.Name);
        return Ok(currentUser.Id);
    }
}
// public string Get(int id)
// {
//     return JsonSerializer.Serialize(CreatingUsers.Users[id]);
// }
// public User Get(int id)
// {
//     return CreatingUsers.Users[id];
// }
    
    
    
//POST /user
// [HttpPost]
// public async Task<IActionResult> Post([FromBody] User reqBody)
// {
//     _context.User.Add(reqBody);
//     await _context.SaveChangesAsync();
//     return Ok(_context.User);
// }
//