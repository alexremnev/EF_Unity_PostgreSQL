using System;
using System.Data.Entity;
using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Context
{
    public class OAuthContext : DbContext, IOAuthContext
    {
        public OAuthContext() : base("QuickBooks") { }
        public DbSet<OAuth> OAuths { get; set; }
        public DbContext GetContext => this;
    }
}