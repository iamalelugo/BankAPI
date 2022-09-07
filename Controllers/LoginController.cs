using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.DataBankModels;
using BankAPI.Data.DTOS;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController: ControllerBase{
    private readonly LoginService _loginservice;
    private readonly IConfiguration config;
    public LoginController(LoginService context, IConfiguration config){
        this._loginservice = context;
        this.config = config;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> LogIn(AdminDto adminDto){
        var admin = await _loginservice.GetAdmin(adminDto);

        if(admin is null)
        return BadRequest(new{message = "Credenciales invalidas"});

        string jwtToken = GenerateToken(admin);

        return Ok(new { token = jwtToken});
    }

    private string GenerateToken(Administrator admin){
        var claims = new []{
            new Claim(ClaimTypes.Name, admin.Name),
            new Claim(ClaimTypes.Email, admin.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var securityToken = new JwtSecurityToken
        (
            claims: claims,
            expires : DateTime.Now.AddMinutes(60),
            signingCredentials : creds);

        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}