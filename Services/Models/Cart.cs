using System.Collections.Generic;
using System.Linq;

namespace Plutus

{
    public class Cart
    {
        public int CartId { get; set; }
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
        public void ChangeState(int index) => _cartParts[index].State = !_cartParts[index].State;
        public void EditExpense(int i, string name, double price, string category)
        {
            var editedExp = new CartExpense(name, price, category, _cartParts[i].State);
            _cartParts[i] = editedExp;
        }
    }
}
