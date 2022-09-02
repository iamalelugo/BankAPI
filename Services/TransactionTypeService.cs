using BankAPI.Data;
using BankAPI.DataBankModels;

namespace BankAPI.Services;

public class TransactionTypeService{
    private readonly BankContext _context;

    public TransactionTypeService(BankContext context){
        _context = context;
    }

    public IEnumerable<TransactionType> GetAll(){
        return _context.TransactionTypes.ToList();
    }

    public TransactionType? GetById(int id){
        return _context.TransactionTypes.Find(id);
    }

    public TransactionType Create(TransactionType newTransactionType){
        _context.TransactionTypes.Add(newTransactionType);
        _context.SaveChanges();
        return newTransactionType;
    }

    public void Update(int id, TransactionType transactionType){
        var existingTransactionType = GetById(id);
        if(existingTransactionType is not null){
             existingTransactionType.Name = transactionType.Name;
            _context.SaveChanges();
        }
    }

    public void Delete(int id){
        var transactionTypeToDelete = GetById(id);
        if(transactionTypeToDelete is not null){
            _context.Remove(id);
            _context.SaveChanges();
        }
    }
}