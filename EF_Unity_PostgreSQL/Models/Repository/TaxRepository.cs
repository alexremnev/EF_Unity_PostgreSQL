using System;
using System.Collections.Generic;
using System.Linq;
using EF_Unity_PostgreSQL.Models.Context;
using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Repository
{
    public class TaxRepository : GenereicRepository<TaxRate>, ITaxRepository
    {
        public TaxRepository(ITaxRateContext context) : base(context.GetContext, NameEntity)
        {
        }
        private const string NameEntity = "TaxRate";
        public TaxRate GetByCountrySubDivisionCode(string state)
        {
            TaxRate taxRate;
            if (string.IsNullOrEmpty(state)) return null;

            try
            {
                taxRate = _dbSet.SingleOrDefault(m => m.CountrySubDivisionCode == state);
            }
            catch (Exception e)
            {
                Log.Error("Exception occured when application tried to get the product by name", e);
                throw;
            }

            return taxRate;
        }

        public IList<TaxRate> List()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (Exception e)
            {
                Log.Error($"Exception occured when application tried to get the list of {NameEntity}s from database", e);
                throw;
            }
        }
    }
}