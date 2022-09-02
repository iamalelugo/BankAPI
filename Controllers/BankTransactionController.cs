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
    public IEnumerable<BankTransaction> Get(){
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<BankTransaction> GetById(int id){
        var transaction = _service.GetById(id);

        if(transaction is not null){
            return transaction;
        }
        else{
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(BankTransaction transaction){
        var newTransaction = _service.Create(transaction);
        return CreatedAtAction(nameof(GetById), new{id = newTransaction.Id}, transaction);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int Id, BankTransaction transaction){
        if(Id != transaction.Id){
            return BadRequest();
        }

        var existingTransaction = _service.GetById(Id);
        if(existingTransaction is not null){
            _service.Update(Id, transaction);
            return NoContent();
        }
        else{
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id){
        var transactionToDelete = _service.GetById(id);
        if(transactionToDelete is not null){
            _service.Delete(id);
            return NoContent();
        }
        else{
            return NotFound();
        }
    }
}