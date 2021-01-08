
using System.Runtime.Serialization;

namespace Plutus
{
    public class CartExpense
    {
        public int ExpenseId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public bool State { get; set; }

        public CartExpense(string name, double price, string category, bool state)
        {
            Name = name;
            Price = price;
            Category = category;
            State = state;
        }
    }
}
