using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.DataBankModels;

namespace BankAPI.Controllers;

[ApiController]
[Route("controller")]
public class BankTransactionController: ControllerBase{
    private readonly BankTransactionService _service;

    public BankTransactionController(BankTransactionService context){
        _service = context;
    }

    [HttpGet]
    public async Task<IEnumerable<BankTransaction>> Get(){
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BankTransaction>> GetById(int id){
        var transaction = await _service.GetById(id);

        if(transaction is not null){
            return transaction;
        }
        else{
            return BankTransactionNotFound(id);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(BankTransaction transaction){
        var newTransaction = await _service.Create(transaction);
        return CreatedAtAction(nameof(GetById), new{id = newTransaction.Id}, transaction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BankTransaction transaction){
        if(id != transaction.Id){
            return BadRequest();
        }

        var existingTransaction = await _service.GetById(id);
        if(existingTransaction is not null){
            await _service.Update(id, transaction);
            return NoContent();
        }
        else{
            return BankTransactionNotFound(id);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var transactionToDelete = await _service.GetById(id);
        if(transactionToDelete is not null){
            await _service.Delete(id);
            return NoContent();
        }
        else{
            return BankTransactionNotFound(id);
        }
    }

    public NotFoundObjectResult BankTransactionNotFound(int id){
        return NotFound(new{ message = $"La transaccion con ID = {id} no fue encontrada"});
    }
}