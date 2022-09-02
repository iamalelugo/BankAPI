using BankAPI.Data;
using BankAPI.DataBankModels;

namespace BankAPI.Services;

public class BankTransactionService{
    private readonly BankContext _context;
    public BankTransactionService(BankContext context){
        _context = context;
    }

    public IEnumerable<BankTransaction> GetAll(){
        return _context.BankTransactions.ToList();
    }

    public BankTransaction? GetById(int id){
        return _context.BankTransactions.Find(id);
    }

    public BankTransaction Create(BankTransaction newTransaction){
        _context.BankTransactions.Add(newTransaction);
        _context.SaveChanges();
        return newTransaction;
    }

    public void Update(int id, BankTransaction transaction){
        var existingTransaction = GetById(id);
        if(existingTransaction is not null){
              existingTransaction.Amount = transaction.Amount;
              existingTransaction.ExternalAccount = transaction.ExternalAccount;
              _context.SaveChanges();
        }
    }

    public void Delete(int id){
        var transactionToDelete= GetById(id);
        if(transactionToDelete is not null){
            _context.Remove(transactionToDelete);
            _context.SaveChanges();
        }
    }
}