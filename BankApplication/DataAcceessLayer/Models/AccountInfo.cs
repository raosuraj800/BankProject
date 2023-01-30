using System;
using System.Collections.Generic;

namespace DBLayer.Models
{
    public partial class AccountInfo
    {
        public AccountInfo()
        {
            TransactionInfos = new HashSet<TransactionInfo>();
        }

        public string AccountId { get; set; } = null!;
        public string BankId { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public string AccountEmail { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
        public string AccountRole { get; set; } = null!;
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Bank Bank { get; set; } = null!;
        public virtual ICollection<TransactionInfo> TransactionInfos { get; set; }
    }
}
