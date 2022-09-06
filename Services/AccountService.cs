using BankAPI.Data;
using BankAPI.DataBankModels;
using Microsoft.EntityFrameworkCore;
namespace BankAPI.Services;

public class AccountService{
    private readonly BankContext _context;
    public AccountService(BankContext context){
        _context = context;
    }

    public async Task<IEnumerable<Account>> GetAll(){
        return await _context.Accounts.ToListAsync();
    }

    public async Task<Account?> GetById(int id){
        return await _context.Accounts.FindAsync(id);
    }

    public async Task<Account> Create(Account newAccount){
        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();
        return newAccount;
    }

    public async Task Update(int id, Account account){
        var existingAccount = await GetById(id);
        if(existingAccount is not null){
            existingAccount.Balance = account.Balance;
            existingAccount.AccountType = account.AccountType;
            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id){
        var accountToDelete = await GetById(id);
        if(accountToDelete is not null){
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }
    }
}