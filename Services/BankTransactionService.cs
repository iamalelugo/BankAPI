using BankAPI.Data;
using BankAPI.DataBankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;

public class BankTransactionService{
    private readonly BankContext _context;
    public BankTransactionService(BankContext context){
        _context = context;
    }

    public async Task<IEnumerable<BankTransaction>> GetAll(){
        return await _context.BankTransactions.ToListAsync();
    }

    public async Task<BankTransaction?> GetById(int id){
        return await _context.BankTransactions.FindAsync(id);
    }

    public async Task<BankTransaction> Create(BankTransaction newTransaction){
        _context.BankTransactions.Add(newTransaction);
        await _context.SaveChangesAsync();
        return newTransaction;
    }

    public async Task Update(int id, BankTransaction transaction){
        var existingTransaction = await GetById(id);
        if(existingTransaction is not null){
              existingTransaction.Amount = transaction.Amount;
              existingTransaction.ExternalAccount = transaction.ExternalAccount;
              await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id){
        var transactionToDelete= await GetById(id);
        if(transactionToDelete is not null){
            _context.Remove(transactionToDelete);
            await _context.SaveChangesAsync();
        }
    }
}