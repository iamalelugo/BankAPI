using BankAPI.Services;
using BankAPI.DataBankModels;
using Microsoft.AspNetCore.Mvc;
using BankApi.Data.DTOS.AccountDtoIn;
using BankAPI.Data.DTOS.AccountDtoOut;
using Microsoft.AspNetCore.Authorization;

namespace BankAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountController: ControllerBase{
    private readonly ClientService clientService;
    private readonly AccountTypeService accountTypeService;
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService, ClientService clientService, AccountTypeService accountTypeService ){
        this._accountService = accountService;
        this.accountTypeService = accountTypeService;
        this.clientService = clientService;
    }

    [HttpGet("getall")]
    public async Task<IEnumerable<AccountDtoOut>> Get(){
        return await _accountService.GetAll();
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<AccountDtoOut>> GetById(int id){
        var account = await _accountService.GetDtoById(id);

          if(account is null)
            return AccountNotFound(id);

        return account;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(AccountDtoIn account){
        String validationResult = await ValidateAccount(account);

        if(!validationResult.Equals("Valid"))
            return BadRequest(new {message = validationResult});

        var newAccount = await _accountService.Create(account);

        return CreatedAtAction(nameof(GetById), new{id = newAccount.Id}, account);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, AccountDtoIn account){
        if(id != account.Id){
            return BadRequest(new {message = $"El ID ({id}) de la URL no coincide con el ID ({account.Id})"});
        }

        var existingAccount = await _accountService.GetById(id);

        if(existingAccount is not null){
            string validationResult = await ValidateAccount(account);

            if(!validationResult.Equals("Valid")){
                return BadRequest();
            }
            else{
                await _accountService.Update(id, account);
                return NoContent();
            }
        }
        else{
            return AccountNotFound(id);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id){
        var accountToDelete = await _accountService.GetById(id);
        if(accountToDelete is not null){
            await _accountService.Delete(id);
            return Ok();
        }
        else{
            return AccountNotFound(id);
        }
    }

     public NotFoundObjectResult AccountNotFound(int id){
        return NotFound(new{ message = $"La cuenta con ID = {id} no existe"});
    }

    public async Task<String> ValidateAccount(AccountDtoIn account){
        string result = "Valid";

        var accountType = await accountTypeService.GetById(account.AccountType);

        if(accountType is null)
         result = $"El tipo de cuenta {accountType} no existe.";

        var clientID = account.ClientId.GetValueOrDefault();
        var client = await clientService.GetById(clientID);

        if(client is null)
         result = $"El cliente con id = {clientID} no existe.";

        return result;
    }
}