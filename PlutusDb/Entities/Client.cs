using Db.Entities;
using System.Collections.Generic;

namespace PlutusDb.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Email { get; set; }
        public virtual List<Budget> Budgets { get; set; }
        public virtual List<Goal> Goals { get; set; }
        public virtual List<Expense> Payments { get; set; }
        public virtual List<ScheduledPayment> ScheduledPayments { get; set; }
    }
}
