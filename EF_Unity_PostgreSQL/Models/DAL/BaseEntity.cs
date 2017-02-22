using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Unity_PostgreSQL.Models.DAL
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string CustomerName { get; set; }
        public string ShipToAddress { get; set; }
        [ForeignKey("ReportId")]
        public IList<LineItem> LineItems { get; set; }
    }
}