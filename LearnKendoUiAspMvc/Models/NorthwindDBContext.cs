using System.Data.Entity;

namespace LearnKendoUiAspMvc.Models
{
    public class NorthwindDbContext : DbContext
    {
        public NorthwindDbContext() : base("NorthwindDB")
        { }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}