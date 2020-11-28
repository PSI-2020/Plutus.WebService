
namespace Plutus
{
    public class CartExpense
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public bool Active { get; set; }

        public CartExpense(string name, double price, string category)
        {
            Name = name;
            Price = price;
            Category = category;
            Active = true;
        }
    }
}
