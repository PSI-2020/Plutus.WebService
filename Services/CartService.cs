using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Plutus.WebService
{
    public class CartService
    {
        private Cart _currentCart;
        private List<Cart> _carts;
        private readonly FileManager _fm = new FileManager();
        private string _cartLoadMessage;

        public CartService() => _carts = LoadCarts();

        public List<String> GiveCarts()
        {
            var names = new List<String>();
            foreach(Cart cart in _carts)
            {
                names.Add(cart.GiveName());
            }
            return names;
        }

        public List<CartExpense> GiveExpenses(int index)
        {
            var expenses = new List<CartExpense>();
            var count = _carts[index].GiveElementC();
            for(var i = 0; i < count; i++)
            {
                expenses.Add(_carts[index].GiveExpense(i));
            }
            return expenses;
        }

        public string GiveLoadMessage()
        {
            var message = _cartLoadMessage;
            _cartLoadMessage = "";
            return message;
        }

        public void NewCart() => _currentCart = new Cart();
        public void AddExpenseToCart(CurrentInfoHolder cih)
        {
            var expense = new CartExpense(cih.CurrentName, double.Parse(cih.CurrentAmout), cih.CurrentCategory);
            _currentCart.AddExpense(expense);
        }

        public int GiveCurrentCartElemCount() => _currentCart.GiveElementC();

        public CartExpense GiveCurrentElemAt(int i) => _currentCart.GiveExpense(i);

        public void RemoveExpenseCurrentAt(int i) => _currentCart.RemoveExpense(i);

        public void SetCurrentName(string name) => _currentCart.ChangeName(name);
        public void AddCurrentCart()
        {
            _carts.Add(_currentCart);
            SaveCarts();
        }

        public string GiveCurrentName() => _currentCart.GiveName();
        public int GiveCartCount() => _carts.Count;

        public string VerifyName(string name, string prevname) => _carts.Where(x => ((x.GiveName() == name) && (x.GiveName() != prevname))).Any() ? "Cart name already taken" : "";

        public string GiveCartNameAt(int i) => _carts[i].GiveName();

        public void CurrentCartSet(int i) => _currentCart = _carts[i];

        public void SaveCartChanges(int i)
        {
            _carts[i] = _currentCart;
            SaveCarts();
        }

        public void DeleteCurrent()
        {
            _ = _carts.Remove(_currentCart);
            SaveCarts();
        }

        public void ChargeCart()
        {
            var ps = new PaymentService(_fm);
            for (var i = 0; i < _currentCart.GiveElementC(); i++)
            {
                var expense = _currentCart.GiveExpense(i);
                ps.AddCartPayment(expense.Name, expense.Price, expense.Category);
            }
        }
        private void SaveCarts()
        {
            var cartsXml = new List<XElement>();
            var index = 0;
            foreach (var cart in _carts)
            {
                var expenseXml = new List<XElement>();
                for (var i = 0; i < cart.GiveElementC(); i++)
                {
                    var expense = cart.GiveExpense(i);
                    var expenseX = new XElement("Expense",
                        new XElement("Name", expense.Name),
                        new XElement("Amount", expense.Price),
                        new XElement("Category", expense.Category),
                        new XElement("Activity", expense.Active));

                    expenseXml.Add(expenseX);
                }
                var cartXml = new XElement("Cart" + index,
                    new XElement("CartName", cart.GiveName()),
                    new XElement("Expenses", expenseXml));
                cartsXml.Add(cartXml);
                index++;
            }
            var cartsStored = new XElement("carts", cartsXml);
            _fm.SaveCarts(cartsStored);
        }
        public void SaveCarts(int index, string name, List<CartExpense> cartExpenses)
        {
            var cart = new Cart();
            for(var i = 0; i < cartExpenses.Count; i++)
            {
                cart.AddExpense(cartExpenses[i]);
            }
            _carts[index] = cart;
            SaveCarts();
        }

        private List<Cart> LoadCarts()
        {
            var cartsList = new List<Cart>();
            var cartsStored = _fm.LoadCarts();
            if (cartsStored == null)
            {
                _cartLoadMessage = "";
                return cartsList;
            }
            var cartsXml = cartsStored.Elements();
            foreach (var cart in cartsXml)
            {
                var specificCart = new Cart();
                var cartName = (string)cart.Element("CartName");
                specificCart.ChangeName(cartName);
                var expenses = cart.Element("Expenses");
                var allExpenses = expenses.Elements();
                foreach (var expense in allExpenses)
                {
                    var cartExpense = new CartExpense(
                        (string)expense.Element("Name"),
                        (double)expense.Element("Amount"),
                        (string)expense.Element("Category")
                        );
                     specificCart.AddExpense(cartExpense);
                }
                cartsList.Add(specificCart);
            }
            _cartLoadMessage = "Carts Loaded";
            return cartsList;
            
        }

        public Cart StartShopping() => _currentCart;

    }
}
