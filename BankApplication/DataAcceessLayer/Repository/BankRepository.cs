using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using DBLayer.DataModel;
using DBLayer.Models;
using DBLayer.UnitOfWork;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Repository
{
    public class BankRepository : GenericRepository<BankDataModel>, IBankRepository
    {
        public BankRepository(IUnitOfWork<BankDatabaseContext> unitOfWork) : base(unitOfWork)
        {
        }

        public BankRepository(BankDatabaseContext context) : base(context)
        {
        }

        public List<BankDataModel> GetAllBankDetails()
        {
            var result = Context.Banks.Where(r=>r.IsActive).ToList();
            return result.Adapt<List<BankDataModel>>();
        }

        public UserCredential VerifyUser(string UserName,string Password)
        {
            var result = Context.AccountInfos.FirstOrDefault(r => r.AccountName == UserName && r.AccountPassword == Password);
            UserCredential user = new UserCredential();
            user.UserName = result.AccountName;
            user.Password = result.AccountPassword;
            user.Role = result.AccountRole;
            user.Email= result.AccountEmail;
            //var x = Context.vali
            return user;

        }
        public string? GetBankIdByBankName(string BankName)
        {
            var result = Context.Banks.FirstOrDefault(r => r.IsActive && r.BankName == BankName)?.BankId;
            return result;
        }

        public bool CheckIfAccountExists(string UserName,string BankName)
        {
            var BankId = GetBankIdByBankName(BankName); ;
            var result = Context.AccountInfos.Any(r => r.AccountName == UserName && r.BankId == BankId);
            return result;
        }
        public async Task<AccountInfoDataModel> GetAccountDetails(string UserName, string BankName)
        {
            var BankId = GetBankIdByBankName(BankName); ;
            var result =await Context.AccountInfos.AsNoTracking().FirstOrDefaultAsync(r => r.AccountName == UserName && r.BankId == BankId);
            var response = result.Adapt<AccountInfoDataModel>();
            return response;
        }
        public async Task<decimal> CheckBalance(string Email)
        {
            
            var result =await Context.AccountInfos.FirstOrDefaultAsync(r => r.AccountEmail == Email);
            return result == null ? 0 : result.Balance;
        }
        public async Task<AccountInfoDataModel> GetAccountInfoByEmail(string Email)
        {

            var result =await Context.AccountInfos.AsNoTracking().FirstOrDefaultAsync(r => r.AccountEmail == Email);
            var response = result.Adapt<AccountInfoDataModel>();
            return response;
        }
    }
    }

