using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.DataBankModels;

namespace BankAPI.Controllers;

[ApiController]
[Route("controller")]
public class TransactionTypeController: ControllerBase{
    private readonly TransactionTypeService _service;
    public TransactionTypeController(TransactionTypeService context){
        _service = context;
    }

    [HttpGet]
    public IEnumerable<TransactionType> Get(){
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<TransactionType> GetById(int id){
        var transactionType = _service.GetById(id);

        if(transactionType is not null){
            return transactionType;
        }
        else{
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Create(TransactionType transactionType){
        var newTransactionType = _service.Create(transactionType);

        return CreatedAtAction(nameof(GetById), new{id = transactionType.Id}, transactionType);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int Id, TransactionType transactionType){
        if( Id != transactionType.Id){
            return BadRequest();
        }

        var transactionTypeToUpdate = _service.GetById(Id);
        if(transactionTypeToUpdate is not null){
            _service.Update(Id, transactionType);
            return NoContent();
        }
        else{
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id){
        var transactionTypeToDelete = _service.GetById(id);
        if(transactionTypeToDelete is not null){
            _service.Delete(id);
            return Ok();
        }
        else{
            return NotFound();
        }
    }
}