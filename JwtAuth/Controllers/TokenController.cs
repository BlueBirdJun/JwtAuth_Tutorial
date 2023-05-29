using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuth.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class TokenController:ControllerBase
{
    private const string TokenSecret = "dotnetdevforeverdotnetdevforeverdotnetdevforeverdotnetdevforeverdotnetdevforever";

    [HttpGet]
    [Route("Token")]
    public IActionResult GenerateToken()
    {
        var tokenhandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(TokenSecret);
        var timespan = TimeSpan.FromHours(8);
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Sub,"dotnetdev"),
            new (JwtRegisteredClaimNames.Email,"dot@korea.com"),
            new ("Userid","blueBird"),
            new ("admin","true"),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(timespan),
            Issuer = "https://dotnetdeve.com",
            Audience = "https://dotnetdeve.com/bluebird",
            SigningCredentials =new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenhandler.CreateToken(tokenDescriptor);
        var jwt = tokenhandler.WriteToken(token);
        return Ok(jwt);
    }
    
}



public class TokenGenerateRequest
{

}