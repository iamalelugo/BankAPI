using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAPI.DataBankModels
{
    public partial class AccountType
    {
        public AccountType()
        {
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }

         [MaxLength (100, ErrorMessage = "El nombre debe ser mayor a 100 caracteres.")]
        public string Name { get; set; } = null!;
        public DateTime RegDate { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
