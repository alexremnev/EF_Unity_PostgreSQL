using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EF_Unity_PostgreSQL.Models
{
    public interface ITaxRateContext
    {
        DbSet<TaxRate> TaxRates { get; set; }
    }
}