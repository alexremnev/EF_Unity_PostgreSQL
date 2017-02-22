using System;
using System.Data.Entity;
using System.Linq;
using EF_Unity_PostgreSQL.Models.Context;
using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Repository
{
    public class OAuthRepository : GenereicRepository<OAuth>, IOAuthRepository
    {
        public OAuthRepository(IOAuthContext context) : base(context.GetContext)
        {
        }

        public OAuth Get()
        {
            try
            {
                return _dbSet.AsQueryable().SingleOrDefault();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Delete()
        {
            try
            {
                var entity = Get();
                if (entity == null) return;
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}