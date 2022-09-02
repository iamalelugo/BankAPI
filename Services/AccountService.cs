using BankAPI.Data;
using BankAPI.DataBankModels;

namespace BankAPI.Services;

public class AccountService{
    private readonly BankContext _context;
    public AccountService(BankContext context){
        _context = context;
    }

    public IEnumerable<Account> GetAll(){
        return _context.Accounts.ToList();
    }

    public Account? GetById(int id){
        return _context.Accounts.Find(id);
    }

    public Account Create(Account newAccount){
        _context.Accounts.Add(newAccount);
        _context.SaveChanges();
        return newAccount;
    }

    public void Update(int id, Account account){
        var existingAccount = GetById(id);
        if(existingAccount is not null){
            existingAccount.Balance = account.Balance;
            existingAccount.AccountType = account.AccountType;
            _context.SaveChanges();
        }
    }

    public void Delete(int id){
        var accountToDelete = GetById(id);
        if(accountToDelete is not null){
            _context.Remove(id);
            _context.SaveChanges();
        }
    }
}