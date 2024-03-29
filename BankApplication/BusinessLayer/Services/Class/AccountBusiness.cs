﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DBLayer.DataModel;
using DBLayer.Models;
using DBLayer.Repository;
using DBLayer.UnitOfWork;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Logging;

namespace Services.Services.Class
{
    public class AccountBusiness : IAccountBusiness
    {

        private UnitOfWork<BankDatabaseContext> unitOfWork = new UnitOfWork<BankDatabaseContext>();
        private GenericRepository<AccountInfo> repository;
        private GenericRepository<TransactionInfo> Transactionrepository;
        private IBankRepository bankRepo;
        private Microsoft.Extensions.Logging.ILogger log;
        public AccountBusiness(Microsoft.Extensions.Logging.ILoggerFactory logs)
        {
            //  If you want to use Generic Repository with Unit of work
            repository = new GenericRepository<AccountInfo>(unitOfWork);
            Transactionrepository = new GenericRepository<TransactionInfo>(unitOfWork);
            //If you want to use Specific Repository with Unit of work
            bankRepo = new BankRepository(unitOfWork);
            this.log = logs.CreateLogger<AccountBusiness>();

        }

        public string GetRandomPassword(int length)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
        public async Task<string> InsertAccountDetails(AccountInfoDataModel Account)
        {
            try
            {
                var Result = Account.Adapt<AccountInfo>();
                repository.Insert(Result);
                unitOfWork.Save();
                log.LogInformation("Inserted Successfully");
                return "Added";

                
            }
            catch (Exception ex)
            {
                log.LogError(" "+ex);
                throw ex;
            }
        }
        public async Task<bool> CheckIfNameExists(string UserName, string BankName)
        {
            var result = bankRepo.CheckIfAccountExists(UserName, BankName);
            return result;
        }

        public async Task<decimal> GetUsersBalance(string Email)
        {
            var result =await bankRepo.CheckBalance(Email);
            log.LogInformation($"{result}");
            return result;
        }
        public async Task<AccountInfoDataModel> GetAccountInfoByEmail(string Email) {
            try
            {
                var result =await bankRepo.GetAccountInfoByEmail(Email);
                return result;
            } catch(Exception ex)
            {
                
                throw ex;
            }
        }
        
        public async Task<string> WithdrawOrDepositMoney(AccountInfoDataModel Account, decimal money, bool IsWithdraw,string? CurrencyType)
        {
            try
            {
                if (IsWithdraw)
                    Account.Balance -= money;
                else
                {
                    var Rates = await bankRepo.GetCurrencyRate(CurrencyType);
                    Account.Balance += Rates*money;
                }
                Account.UpdatedBy = "Suraj";
                var AccountInfo = Account.Adapt<AccountInfo>();
                repository.Update(AccountInfo);
                unitOfWork.Save();
                log.LogInformation($"AccountInfo: {AccountInfo}");
                return "Transaction Completed";
            }
            catch (Exception ex)
            {
                log.LogError("Error"+ex);
                throw ex;
            }
        }
        public async Task<string> Transaction(AccountInfoDataModel Account, decimal money, bool IsRTGS,bool IsSameBank, string? BankName, string? AccountName)
            {
            try
            {
                string? AccountId;
                string? BankId;
                if (IsSameBank)
                {
                    if (IsRTGS)
                        Account.Balance += money;
                    else
                       Account.Balance += money-((money*5)/100);
                    BankId = null;
                    AccountId = null;
                }
                else
                {
                    if (bankRepo.CheckIfAccountExists(AccountName, BankName))
                    {
                        var toAccount =await bankRepo.GetAccountDetails(AccountName, BankName);
                        AccountId= toAccount.AccountId;
                        BankId= toAccount.BankId;
                        toAccount.Balance += money;
                        if (IsRTGS)
                        {
                            Account.Balance -= money - ((money * 2) / 100); ;
                        }
                        else
                        {
                            Account.Balance -= money - ((money * 6) / 100); ;
                        }
                        toAccount.UpdatedBy = "Suraj";
                        var toAccountInfo = toAccount.Adapt<AccountInfo>();
                        repository.Update(toAccountInfo);
                        unitOfWork.Save();
                    }
                    else
                    {
                        return "Bank does not exist/Bank does not contain the Account";
                    }
                }
                Account.UpdatedBy = "Suraj";
                
                var AccountInfo = Account.Adapt<AccountInfo>();
                repository.Update(AccountInfo);
                unitOfWork.Save();
                var Transaction = new TransactionInfoDataModel();
                Transaction.BankId = BankId;
                Transaction.AccountId = AccountId;
                Transaction.TransactionId = "TXN" +  AccountInfo.AccountId+AccountInfo.BankId+ string.Concat(DateTime.Now.ToString().Where(c => !char.IsWhiteSpace(c)));
                Transaction.IsRtgs= IsRTGS;
                Transaction.Amount = money;
                Transaction.IsActive= true;
                Transaction.CreatedBy = "suraj";
                var result = Transaction.Adapt<TransactionInfo>();
                Transactionrepository.Insert(result);
                unitOfWork.Save();
                return "Transaction Completed";
            }
            catch (Exception ex)
            {
                return "";
                throw ex;
            }
        }
        public async Task<bool> CheckIfCurrencyExists(string Currency)
        {
            var result = await bankRepo.CheckIfCurrencyExists(Currency);
            return result;
        }
        public IEnumerable<AccountInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public AccountInfo GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(AccountInfo obj)
        {
            throw new NotImplementedException();
        }

        public void Update(AccountInfo obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(AccountInfo obj)
        {
            throw new NotImplementedException();
        }

        
    }
    public class ApplicationLogs
    {
    }
}
