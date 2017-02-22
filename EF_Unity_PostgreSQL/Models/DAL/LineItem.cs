namespace EF_Unity_PostgreSQL.Models.DAL
{
    public class LineItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}