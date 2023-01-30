using System;
using System.Collections.Generic;
using System.Text;
using DBLayer.Models;

namespace DBLayer.DataModel
{
    public class BankDataModel :  BaseEntity
    {
        public string BankId { get; set; }
        public string BankName { get; set; }
        public bool IsActive { get; set; }

    }
    public class BankData
    {
        public string BankId { get; set; }
        public string BankName { get; set; }
     

    }
}
