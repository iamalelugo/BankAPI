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
    public IEnumerable<Account> Get(){
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Account> GetById(int id){
        var account = _service.GetById(id);

          if(account is not null){
            return account;
        }
        else{
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(Account account){
        var newAccount = _service.Create(account);

        return CreatedAtAction(nameof(GetById), new{id = newAccount.Id}, account);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int Id, Account account){
        if(Id != account.Id){
            return BadRequest();
        }

        var existingAccount = _service.GetById(Id);
        if(existingAccount is not null){
            _service.Update(Id, account);
            return NoContent();
        }
        else{
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id){
        var accountToDelete = _service.GetById(id);
        if(accountToDelete is not null){
            _service.Delete(id);
            return Ok();
        }
        else{
            return NotFound();
        }
    }
}