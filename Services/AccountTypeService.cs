using BankAPI.Data;
using BankAPI.DataBankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;

public class AccountTypeService{
    private readonly BankContext _context;
    public AccountTypeService(BankContext context){
        _context = context;
    }

    public async Task<IEnumerable<AccountType>> GetAll(){
        return await _context.AccountTypes.ToListAsync();
    }

    public async Task<AccountType?> GetById(int id){
        return await _context.AccountTypes.FindAsync(id);
    }

    public async Task<AccountType> Create(AccountType newAccountType){
        _context.AccountTypes.Add(newAccountType);
        await _context.SaveChangesAsync();
        return newAccountType;
    }

    public async Task Update(int id, AccountType accountType){
        var existingAccountType = await GetById(id);
        if(existingAccountType is not null){
            existingAccountType.Name = accountType.Name;
            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id){
        var typeToDelete = await GetById(id);
        if(typeToDelete is not null){
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }
    }
}