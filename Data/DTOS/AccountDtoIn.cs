namespace BankApi.Data.DTOS.AccountDtoIn;

public class AccountDtoIn{
        public int Id { get; set; }
        public int AccountType { get; set; }
        public int? ClientId { get; set; }
        public decimal Balance { get; set; }
}