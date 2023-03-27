using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer.Models;
using DBLayer.DataModel;
using DBLayer.Repository;

namespace Services.Services
{
    public interface IAccountBusiness:IGenericRepository<AccountInfo>
    {
        string GetRandomPassword(int length);
        Task<string> InsertAccountDetails(AccountInfoDataModel Account);
        Task<bool> CheckIfNameExists(string UserName, string BankName);
        Task<decimal> GetUsersBalance(string Email);
        Task<AccountInfoDataModel> GetAccountInfoByEmail(string Email);
        Task<string> WithdrawOrDepositMoney(AccountInfoDataModel Account, decimal money, bool IsWithdraw,string? CurrencyType);
        Task<string> Transaction(AccountInfoDataModel Account, decimal money, bool IsRTGS, bool IsSameBank, string? BankName, string? AccountName);
        Task<bool> CheckIfCurrencyExists(string Currency);
    }
}
