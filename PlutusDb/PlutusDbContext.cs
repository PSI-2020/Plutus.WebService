using Db.Entities;
using Microsoft.EntityFrameworkCore;
using PlutusDb.Entities;

namespace Db
{
    public class PlutusDbContext : DbContext
    {
        public PlutusDbContext(DbContextOptions<PlutusDbContext> options) : base(options)
        {
        }

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Expense> Payments { get; set; }
        public DbSet<ScheduledPayment> ScheduledPayments { get; set; }
        public DbSet<CartExpense> CartExpenses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<HistoryElement> HistoryElements { get; set; }
        public DbSet<ShoppingExpense> ShoppingExpenses { get; set; }


    }
}
