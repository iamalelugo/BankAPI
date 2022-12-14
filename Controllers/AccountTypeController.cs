using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.DataBankModels;
using Microsoft.AspNetCore.Authorization;

namespace BankAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountTypeController: ControllerBase{
    private readonly AccountTypeService _service;
    public AccountTypeController (AccountTypeService context){
        _service = context;
    }

    [HttpGet("getall")]
    public async Task<IEnumerable<AccountType>> Get(){
        return await _service.GetAll();
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<AccountType>> GetById(int id){
        var accountType = await _service.GetById(id);

        if(accountType is not null){
            return accountType;
        }
        else{
            return AccountTypeNotFound(id);
        }
    }

    [HttpPost("crate")]
    public async Task<IActionResult> Create(AccountType accountType){
        var newAccountType = await _service.Create(accountType);

        return CreatedAtAction(nameof(GetById), new{id = accountType.Id}, accountType);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update (int id, AccountType accountType){
        if(id != accountType.Id){
            return BadRequest();
        }

        var accountTypeToUpdate = await _service.GetById(id);
        if(accountTypeToUpdate is not null){
            await _service.Update(id, accountType);
            return NoContent();
        }
        else{
            return AccountTypeNotFound(id);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id){
        var typeToDelete = await _service.GetById(id);
        if(typeToDelete is not null){
            await _service.Delete(id);
            return Ok();
        }
        else{
            return AccountTypeNotFound(id);
        }
    }

      public NotFoundObjectResult AccountTypeNotFound(int id){
        return NotFound(new{ message = $"El tipo de cuenta con ID = {id} no existe"});
    }
}