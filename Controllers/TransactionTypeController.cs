using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.DataBankModels;
using Microsoft.AspNetCore.Authorization;

namespace BankAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionTypeController: ControllerBase{
    private readonly TransactionTypeService _service;
    public TransactionTypeController(TransactionTypeService context){
        _service = context;
    }

    [HttpGet("getall")]
    public async Task<IEnumerable<TransactionType>> Get(){
        return await _service.GetAll();
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<TransactionType>> GetById(int id){
        var transactionType = await _service.GetById(id);

        if(transactionType is not null){
            return transactionType;
        }
        else{
            return TransactionTypeNotFound(id);
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(TransactionType transactionType){
        var newTransactionType = await _service.Create(transactionType);

        return CreatedAtAction(nameof(GetById), new{id = transactionType.Id}, transactionType);
    }

    [HttpPut("update{id}")]
    public async Task<IActionResult> Update(int id, TransactionType transactionType){
        if( id != transactionType.Id){
            return BadRequest();
        }

        var transactionTypeToUpdate = await _service.GetById(id);
        if(transactionTypeToUpdate is not null){
            await _service.Update(id, transactionType);
            return NoContent();
        }
        else{
            return TransactionTypeNotFound(id);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id){
        var transactionTypeToDelete = await _service.GetById(id);
        if(transactionTypeToDelete is not null){
            await _service.Delete(id);
            return Ok();
        }
        else{
            return TransactionTypeNotFound(id);
        }
    }

    public NotFoundObjectResult TransactionTypeNotFound(int id){
        return NotFound(new{message = $"El tipo de transacción con {id} no existe."});
    }
}