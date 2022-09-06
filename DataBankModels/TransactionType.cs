using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.DataBankModels
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            BankTransactions = new HashSet<BankTransaction>();
        }

        public int Id { get; set; }
         [MaxLength (100, ErrorMessage = "El nombre debe ser mayor a 100 caracteres.")]
        public string Name { get; set; } = null!;
        public DateTime RegDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<BankTransaction> BankTransactions { get; set; }
    }
}
