using BankAPI.Services;
using BankAPI.DataBankModels;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("controller")]
public class AccountController: ControllerBase{
    private readonly AccountService _service;
    public AccountController(AccountService context){
        _service = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Account>> Get(){
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetById(int id){
        var account = await _service.GetById(id);

          if(account is not null){
            return account;
        }
        else{
            return AccountNotFound(id);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Account account){
        var newAccount = await _service.Create(account);

        return CreatedAtAction(nameof(GetById), new{id = newAccount.Id}, account);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Account account){
        if(id != account.Id){
            return BadRequest();
        }

        var existingAccount = await _service.GetById(id);
        if(existingAccount is not null){
            await _service.Update(id, account);
            return NoContent();
        }
        else{
            return AccountNotFound(id);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var accountToDelete = await _service.GetById(id);
        if(accountToDelete is not null){
            await _service.Delete(id);
            return Ok();
        }
        else{
            return AccountNotFound(id);
        }
    }

     public NotFoundObjectResult AccountNotFound(int id){
        return NotFound(new{ message = $"La cuenta con ID = {id} no existe"});
    }
}