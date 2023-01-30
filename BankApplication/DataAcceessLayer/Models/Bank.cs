using System;
using System.Collections.Generic;

namespace DBLayer.Models
{
    public partial class Bank
    {
        public Bank()
        {
            AccountInfos = new HashSet<AccountInfo>();
            TransactionInfos = new HashSet<TransactionInfo>();
        }

        public string BankId { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<AccountInfo> AccountInfos { get; set; }
        public virtual ICollection<TransactionInfo> TransactionInfos { get; set; }
    }
}
