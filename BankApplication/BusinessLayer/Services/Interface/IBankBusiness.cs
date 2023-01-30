using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBLayer.Repository;
using DBLayer.DataModel;
using DBLayer.Models;

namespace Services.Services
{
    
    public interface IBankBusiness 
    {
        public string InsertBankDetails(BankDataModel bank);
        List<BankData> GetAllBank();
        Task<string?> FindBankId(string BankName);
    }
}
