using PlutusDb.Entities;

namespace Db.Entities
{
    public class CartExpense
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public bool State { get; set; }
        public int CartId { get; set; }
        public int CartExpenseId { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
