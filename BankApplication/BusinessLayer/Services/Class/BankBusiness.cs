using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer.DataModel;
using DBLayer.Models;
using DBLayer.Repository;
using DBLayer.UnitOfWork;
using Mapster;

namespace Services.Services
{
     public class BankBusiness :IBankBusiness
    {
        private UnitOfWork<BankDatabaseContext> unitOfWork = new UnitOfWork<BankDatabaseContext>();
        private GenericRepository<Bank> repository;
        private IBankRepository bankRepo;
        public BankBusiness()
        {
            //If you want to use Generic Repository with Unit of work
            repository = new GenericRepository<Bank>(unitOfWork);
            //If you want to use Specific Repository with Unit of work
            bankRepo = new BankRepository(unitOfWork);
        }
        
        
        public string InsertBankDetails(BankDataModel bank)
        {
            try
            {
                var Result = bank.Adapt<Bank>();
                repository.Insert(Result);
                unitOfWork.Save();
                return "Item Added Successfully";
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<BankData> GetAllBank()
        {
            try
            {
                var Details = from s in bankRepo.GetAllBankDetails()
                              select new BankData
                              {
                                  BankId = s.BankId,
                                  BankName = s.BankName
                              };
                var result = Details.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string>? FindBankId(string BankName)
        {
            var BankId = bankRepo.GetBankIdByBankName(BankName);
            return BankId;
        }
    }
}
