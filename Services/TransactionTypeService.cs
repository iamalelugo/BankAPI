using BankAPI.Data;
using BankAPI.DataBankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;

public class TransactionTypeService{
    private readonly BankContext _context;

    public TransactionTypeService(BankContext context){
        _context = context;
    }

    public async Task<IEnumerable<TransactionType>> GetAll(){
        return await _context.TransactionTypes.ToListAsync();
    }

    public async Task<TransactionType?> GetById(int id){
        return await _context.TransactionTypes.FindAsync(id);
    }

    public async Task<TransactionType> Create(TransactionType newTransactionType){
        _context.TransactionTypes.Add(newTransactionType);
        await _context.SaveChangesAsync();
        return newTransactionType;
    }

    public async Task Update(int id, TransactionType transactionType){
        var existingTransactionType = await GetById(id);
        if(existingTransactionType is not null){
             existingTransactionType.Name = transactionType.Name;
            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id){
        var transactionTypeToDelete = await GetById(id);
        if(transactionTypeToDelete is not null){
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }
    }
}