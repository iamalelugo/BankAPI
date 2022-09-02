using BankAPI.Data;
using BankAPI.DataBankModels;

namespace BankAPI.Services;

public class AccountTypeService{
    private readonly BankContext _context;
    public AccountTypeService(BankContext context){
        _context = context;
    }

    public IEnumerable<AccountType> GetAll(){
        return _context.AccountTypes.ToList();
    }

    public AccountType? GetById(int id){
        return _context.AccountTypes.Find(id);
    }

    public AccountType Create(AccountType newAccountType){
        _context.AccountTypes.Add(newAccountType);
        _context.SaveChanges();
        return newAccountType;
    }

    public void Update(int id, AccountType accountType){
        var existingAccountType = GetById(id);
        if(existingAccountType is not null){
            existingAccountType.Name = accountType.Name;
            _context.SaveChanges();
        }
    }

    public void Delete(int id){
        var typeToDelete = GetById(id);
        if(typeToDelete is not null){
            _context.Remove(id);
            _context.SaveChanges();
        }
    }
}