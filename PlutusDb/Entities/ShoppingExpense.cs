namespace PlutusDb.Entities
{
    public class ShoppingExpense
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public int State { get; set; }
        public int ShoppingExpenseId { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
