using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.DataModel
{
    public class CurrencyChartDataModel
    {
        public string CurrencyType { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class CurrencyModel {
        public string CurrencyType { get; set; }
        public decimal Rate { get; set; }
    }
}
