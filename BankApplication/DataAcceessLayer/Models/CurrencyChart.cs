using System;
using System.Collections.Generic;

namespace DBLayer.Models
{
    public partial class CurrencyChart
    {
        public string CurrencyType { get; set; } = null!;
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
