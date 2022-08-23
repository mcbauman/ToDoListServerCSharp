using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Data;
using ToDoList.Domain;


namespace ToDoList.security;

public class token
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    public token(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public string CreateToken(UserClass user)
    {
        List<Claim> claims = new List<Claim> 
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
        };
        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires:DateTime.Now.AddDays(1),
            signingCredentials:cred);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
    // public string CreateToken(UserClass user)
    // {
    //     List<Claim> claims = new List<Claim>
    //     {
    //         new Claim(ClaimTypes.Name, user.Name)
    //     };
    //     var key = new SymmetricsSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value))
    // }
}
// public string secret = "GanzGeheim";
    // public string GenerateToken(string value)
    // {
    //     var token = JwtBuilder.Create()
    //         .WithAlgorithm(new RS256Algorithm(certificate))
    //         .WithSecret(secret)
    //         .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(24).ToUnixTimeSeconds())
    //         .AddClaim("user", value)
    //         .Encode();
    //
    //     return token;
    // }
    //
    // public string DecodingToken(string token)
    // {
    //     var json = JwtBuilder.Create()
    //         .WithAlgorithm(new RS256Algorithm(certificate))
    //         .WithSecret(secret)
    //         .MustVerifySignature()
    //         .Decode(token);                    
    //     Console.WriteLine(json);
    // }
