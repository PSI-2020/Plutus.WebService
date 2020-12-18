using Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db
{
    public class SmartsaverDbContext : DbContext
    {
        public SmartsaverDbContext(DbContextOptions<SmartsaverDbContext> options) : base(options)
        {
        }

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ScheduledPayment> ScheduledPayments { get; set; }
        public DbSet<CartExpense> CartExpenses { get; set; }
    }
}
