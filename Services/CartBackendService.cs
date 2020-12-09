﻿using System.Collections.Generic;
using System.Xml.Linq;

namespace Plutus.WebService
{
    public class CartBackendService
    {
        private readonly List<Cart> _carts;
        private readonly FileManager _fm = new FileManager();

        public CartBackendService() => _carts = LoadCarts(); 

        public List<string> GiveCartNames()
        {
            var names = new List<string>();
            foreach (var cart in _carts)
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

        public void DeleteCart(int index)
        {
            _carts.RemoveAt(index);
            SaveCarts();
        }

        public void ChargeCart(int index)
        {
            var ps = new PaymentService(_fm);
            for (var i = 0; i < _carts[index].GiveElementC(); i++)
            {
                if (_carts[index].GiveExpense(i).Active)
                {
                    var expense = _carts[index].GiveExpense(i);
                    ps.AddCartPayment(expense.Name, expense.Price, expense.Category);
                }

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
            var newCart = new Cart(name);

            for(var i = 0; i < cartExpenses.Count; i++)
            {
                newCart.AddExpense(cartExpenses[i]);
            }
            if (index < _carts.Count)
            {
                _carts[index] = newCart;
            }
            else
            {
                _carts.Add(newCart);
            }

            SaveCarts();
        }

        private List<Cart> LoadCarts()
        {
            var cartsList = new List<Cart>();
            var cartsStored = _fm.LoadCarts();
            if (cartsStored == null)
            {
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
            return cartsList;
        }

    }
}
