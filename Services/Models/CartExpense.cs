
namespace Plutus.WebService
{
    public class CartExpense
    {
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
