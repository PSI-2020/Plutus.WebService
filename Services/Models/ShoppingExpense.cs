
namespace Plutus
{
    public class ShoppingExpense
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public short State { get; set; }

        public ShoppingExpense(CartExpense expense, short state)
        {
            Name = expense.Name;
            Price = expense.Price;
            Category = expense.Category;
            State = state;
        }
    }
}
