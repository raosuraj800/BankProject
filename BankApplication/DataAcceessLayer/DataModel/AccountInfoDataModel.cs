using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using DBLayer.Models;

namespace DBLayer.DataModel
{
   public class AccountInfoDataModel :BaseDto<AccountInfoDataModel, DBLayer.Models.AccountInfo>
    {
        public string AccountId { get; set; }
        public string BankId { get; set; }
        public string AccountName { get; set; }
        public string AccountEmail { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
        public string AccountRole { get; set; } = null!;
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
       
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
     

    }
    public class UserCredential {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }

        public string? Email { get; set; }
     
    }

    public struct AccountCreateModel {
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public string BankName { get; set; }
    }
    public class BalanceSeek {
        public string BankName { get; set; }
        public string AccountName { get; set; }
    }
    public class TransactionAccount {
        public decimal Amount { get; set; }
        public bool toSameBank { get; set; }
        public string? BankName { get; set; }
        public bool IsRTGS { get; set; }
        public string? AccountName { get; set; }
    }
}
