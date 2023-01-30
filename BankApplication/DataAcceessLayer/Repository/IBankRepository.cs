using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer.DataModel;

namespace DBLayer.Repository
{
     public interface IBankRepository
    {
        List<BankDataModel> GetAllBankDetails();
        UserCredential VerifyUser(string UserName, string Password);
        string? GetBankIdByBankName(string BankName);
        bool CheckIfAccountExists(string UserName, string BankName);

        Task<decimal> CheckBalance(string Email);
        Task<AccountInfoDataModel> GetAccountInfoByEmail(string Email);
        Task<AccountInfoDataModel> GetAccountDetails(string UserName, string BankName);
    }
}
