﻿using System.Data.Entity;
using EF_Unity_PostgreSQL.Models.DAL;

namespace EF_Unity_PostgreSQL.Models.Context
{
    public class ReportContext : DbContext, IReportContext
    {
        public ReportContext() : base("QuickBooks")
        {
        }
        public DbSet<Report> Reports { get; set; }
        public DbContext GetContext => this;
    }
}