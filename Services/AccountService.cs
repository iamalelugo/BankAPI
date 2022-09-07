using BankApi.Data.DTOS.AccountDtoIn;
using BankAPI.Data;
using BankAPI.Data.DTOS.AccountDtoOut;
using BankAPI.DataBankModels;
using Microsoft.EntityFrameworkCore;
namespace BankAPI.Services;

public class AccountService{
    private readonly BankContext _context;
    public AccountService(BankContext context){
        _context = context;
    }

    public async Task<IEnumerable<AccountDtoOut>> GetAll(){
        return await _context.Accounts.Select(a => new AccountDtoOut
        {
            Id = a.Id,
            AccountName = a.AccountTypeNavigation.Name,
            ClientName = a.Client != null ? a.Client.Name : "",
            Balance = a.Balance,
            RegDate = a.RegDate
        }).ToListAsync();
    }

    public async Task<Account?> GetById(int id){
        return await _context.Accounts.FindAsync(id);
    }
    public async Task<AccountDtoOut?> GetDtoById(int id){
        return await _context.Accounts.Where( a => a.Id == id).Select( a => new AccountDtoOut{
            Id = a.Id,
            AccountName = a.AccountTypeNavigation.Name,
            ClientName = a.Client != null ? a.Client.Name : "",
            Balance = a.Balance,
            RegDate = a.RegDate
        }).SingleOrDefaultAsync();
    }

    public async Task<Account> Create(AccountDtoIn newAccountDTO){
        var newAccount = new Account();

        newAccount.AccountType = newAccountDTO.AccountType;
        newAccount.ClientId = newAccountDTO.ClientId;
        newAccount.Balance = newAccountDTO.Balance;

        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();
        return newAccount;
    }

    public async Task Update(int id, AccountDtoIn account){
        var existingAccount = await GetById(id);
        if(existingAccount is not null){
            existingAccount.Balance = account.Balance;
            existingAccount.AccountType = account.AccountType;
            existingAccount.ClientId = account.ClientId;
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