using System;
using System.Collections.Generic;
using System.Text;
using DBLayer.Models;

namespace DBLayer.DataModel
{
    public class TransactionInfoDataModel : BaseEntity
    {
        public string TransactionId { get; set;  }
        public string? AccountId { get; set; }
        public string? BankId { get; set; }
        public bool IsRtgs { get; set; }
        public decimal Amount { get; set; }
        public bool IsWithdrawal { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
    }
}
