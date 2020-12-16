using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    public class Cart
    {
        private readonly List<CartExpense> _cartParts;
        private string _cartName;

        public Cart() => _cartParts = new List<CartExpense>();
        public Cart(string name) : this() => _cartName = name;
        public string GiveName() => _cartName;
        public void ChangeName(string newName) => _cartName = newName;
        public CartExpense GiveExpense(int index) => _cartParts.ElementAt(index);
        public int GiveElementC() => _cartParts.Count;
        public void AddExpense(CartExpense expense) => _cartParts.Add(expense);
        public void RemoveExpense(int number) => _cartParts.RemoveAt(number);
    }
}
