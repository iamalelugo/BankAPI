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
    public LoginController(LoginService context){
        _loginservice = context;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> LogIn(AdminDto adminDto){
        var admin = await _loginservice.GetAdmin(adminDto);

        if(admin is null)
        return BadRequest(new{message = "Credenciales invalidas"});

        return Ok(new { token = "Some value"});
    }
}