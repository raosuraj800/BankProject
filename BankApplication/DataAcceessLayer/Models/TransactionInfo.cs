using System;
using System.Collections.Generic;

namespace DBLayer.Models
{
    public partial class TransactionInfo
    {
        public string TransactionId { get; set; } = null!;
        public string? AccountId { get; set; }
        public string? BankId { get; set; }
        public bool IsRtgs { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual AccountInfo? Account { get; set; }
        public virtual Bank? Bank { get; set; }
    }
}
