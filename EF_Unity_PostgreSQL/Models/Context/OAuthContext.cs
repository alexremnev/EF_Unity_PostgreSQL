using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EF_Unity_PostgreSQL.Models.Business;
using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Context
{
    public class OAuthContext : DbContext, IOAuthContext
    {
        public OAuthContext(ICryptoEngine cryptoEngine) : base("QuickBooks")
        {
            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += ObjectMaterialized;
            _cryptoEngine = cryptoEngine;
        }
        public DbSet<OAuth> OAuths { get; set; }
        public DbContext GetContext => this;
        private readonly ICryptoEngine _cryptoEngine;

        public override int SaveChanges()
        {
            var contextAdapter = ((IObjectContextAdapter)this);
            contextAdapter.ObjectContext.DetectChanges();
            var pendingEntities = contextAdapter.ObjectContext.ObjectStateManager
                .GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                .Where(en => !en.IsRelationship).ToList();

            foreach (var entry in pendingEntities) EncryptEntity(entry.Entity);
            var result = base.SaveChanges();
            foreach (var entry in pendingEntities) DecryptEntity(entry.Entity);
            return result;
        }

        private void ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            DecryptEntity(e.Entity);
        }

        private void EncryptEntity(object entity)
        {
            var encryptedProperties = entity.GetType().GetProperties()
                     .Where(p => p.GetCustomAttributes(typeof(Encrypted), true).Any(a => p.PropertyType == typeof(String)));
            foreach (var property in encryptedProperties)
            {
                var value = property.GetValue(entity) as string;
                if (string.IsNullOrEmpty(value)) continue;
                var encryptedValue = _cryptoEngine.Encrypt(value);
                property.SetValue(entity, encryptedValue);
            }
        }

        private void DecryptEntity(object entity)
        {
            var encryptedProperties = entity.GetType().GetProperties()
                 .Where(p =>
                {
                    return p.GetCustomAttributes(typeof(Encrypted), true).Any(a => p.PropertyType == typeof(string));
                });

            foreach (var property in encryptedProperties)
            {
                var encryptedValue = property.GetValue(entity) as string;
                if (string.IsNullOrEmpty(encryptedValue)) continue;
                var value = _cryptoEngine.Decrypt(encryptedValue);
                Entry(entity).Property(property.Name).OriginalValue = value;
                Entry(entity).Property(property.Name).IsModified = false;
            }
        }
    }
}