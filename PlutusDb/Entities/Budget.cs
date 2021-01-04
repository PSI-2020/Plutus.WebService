using PlutusDb.Entities;

namespace Db.Entities
{
    public class Budget
    {
        public int BudgetId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        //public virtual Client Client { get; set; }
    }
}
