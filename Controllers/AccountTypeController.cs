using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data;
using BankAPI.DataBankModels;

namespace BankAPI.Controllers;

[ApiController]
[Route("controller")]
public class AccountTypeController: ControllerBase{
    private readonly AccountTypeService _service;
    public AccountTypeController (AccountTypeService context){
        _service = context;
    }

    [HttpGet]
    public IEnumerable<AccountType> Get(){
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<AccountType> GetById(int id){
        var accountType = _service.GetById(id);

        if(accountType is not null){
            return accountType;
        }
        else{
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(AccountType accountType){
        var newAccountType = _service.Create(accountType);

        return CreatedAtAction(nameof(GetById), new{id = accountType.Id}, accountType);
    }

    [HttpPut("{id}")]
    public IActionResult Update (int id, AccountType accountType){
        if(id != accountType.Id){
            return BadRequest();
        }

        var accountTypeToUpdate = _service.GetById(id);
        if(accountTypeToUpdate is not null){
            _service.Update(id, accountType);
            return NoContent();
        }
        else{
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id){
        var typeToDelete = _service.GetById(id);
        if(typeToDelete is not null){
            _service.Delete(id);
            return Ok();
        }
        else{
            return NotFound();
        }
    }
}