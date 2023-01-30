using System;
using System.Collections.Generic;

namespace DBLayer.Models
{
    public partial class CurrencyChart
    {
        public string CurrencyType { get; set; } = null!;
        public decimal Rate { get; set; }
    }
}
