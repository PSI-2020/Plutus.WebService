
namespace Plutus.WebService
{
    public class ShoppingExpense
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public int State { get; set; }

        public ShoppingExpense()
        {

        }

        public ShoppingExpense(string name, double price, string category, int state)
        {
            Name = name;
            Price = price;
            Category = category;
            State = state;
        }

        public ShoppingExpense(CartExpense expense, int state)
        {
            Name = expense.Name;
            Price = expense.Price;
            Category = expense.Category;
            State = state;
        }
    }
}
