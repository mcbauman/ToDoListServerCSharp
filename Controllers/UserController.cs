using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Domain;
using ToDoList.security;

namespace ToDoList.Controllers;
[ApiController]
[Route("/[Controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHandler _hashPassword;
    private readonly IConfiguration _configuration;
    private readonly token _token;
    public UserController(ApplicationDbContext context,PasswordHandler hashPassword,IConfiguration configuration, token token)
    {
        _context = context;
        _hashPassword = hashPassword;
        _configuration = configuration;
        _token = token;
    }

    
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] loginReqClass reqBody)
    {
        try
        {
            UserClass currentUser = await _context.Users.FirstAsync(x => x.Name == reqBody.Name);
            if(_hashPassword.PasswordMatch(reqBody.Password,currentUser.Password))
            {
                string token = _token.CreateToken(currentUser);
                return Ok(token);
            }
            return Ok("Wrong Login Data");
            //return BadRequest("Wrong  LoginData");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    //_config über Params übergeben


    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] UserClass reqBody)
    {
        reqBody.Password = _hashPassword.HashPassword(reqBody.Password);
        _context.Users.Add(reqBody);
        await _context.SaveChangesAsync();
        UserClass currentUser = _context.Users.FirstOrDefault(x => x.Name == reqBody.Name);
        string token = _token.CreateToken(currentUser);
        return Ok(token);
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


// [HttpGet]
// public IActionResult GetAll()
// {
//     return Ok(_context.Users);
// }
//
// // GET login?name=name&password=password
// [HttpGet("/login")]
// public IActionResult Get([FromQuery] loginReqClass reqQuery)
// {
//     return Ok(reqQuery);
// }