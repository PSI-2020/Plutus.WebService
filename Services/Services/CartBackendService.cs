using Db;
using Plutus.WebService.IRepos;
using Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    public class CartBackendService : ICartBackendService
    {
        private readonly IPaymentService _paymentService;
        private readonly PlutusDbContext _context;

        public CartBackendService(IPaymentService paymentService, PlutusDbContext context)
        {
            _context = context;
            _paymentService = paymentService;
        }

        public List<CartInfo> GiveCartNames()
        {
            var carts = _context.Carts.ToList();
            var names = new List<CartInfo>();
            foreach (var cart in carts)
            {
                var ci = new CartInfo
                {
                    CartId = cart.CartId,
                    Name = cart.Name
                };
                names.Add(ci);
            }
            return names;
        }

        public List<CartExpense> GiveExpenses(int id)
        {
            var exps = _context.CartExpenses.Where(x => x.CartId == id).ToList();
            var expenses = new List<CartExpense>();
            foreach (var expense in exps)
            {
                var exp = new CartExpense(expense.Name, expense.Price, expense.Category, expense.State)
                {
                    ExpenseId = expense.CartExpenseId
                };
                expenses.Add(exp);
            }
            return expenses;
        }

        public void DeleteCart(int id)
        {
            _context.Carts.Remove(_context.Carts.First(x => x.CartId == id));
            _context.SaveChanges();
        }

        public void ChargeCart(int id)
        {
            var cart = _context.CartExpenses.Where(x => x.CartId == id).ToList();
            foreach(var expense in cart)
            {
                if (expense.State)
                {
                    _paymentService.AddCartPayment(expense.Name, expense.Price, expense.Category.ToString());
                }
            }
        }

        public void RenameCart(int id, string name)
        {

            var cartFromDb = _context.Carts.First(x => x.CartId == id);
            if(name != cartFromDb.Name)
            {
                cartFromDb.Name = name;
                _context.Carts.Update(cartFromDb);
                _context.SaveChanges();

            }
        }

        public void AddExpense(int cartId, CartExpense expense)
        {
            var exp = new Db.Entities.CartExpense
            {
                Name = expense.Name,
                Price = expense.Price,
                Category = expense.Category,
                State = expense.State,
                CartId = cartId
            };

            _context.CartExpenses.Add(exp);
            _context.SaveChanges(); 
        }
        public void EditExpense(int cartId, CartExpense expense)
        {
            var exp = _context.CartExpenses.First(x => ((x.CartId == cartId) && (x.CartExpenseId == expense.ExpenseId)));
            exp.Name = expense.Name;
            exp.Price = expense.Price;
            exp.State = expense.State;
            exp.Category = expense.Category;
            _context.CartExpenses.Update(exp);
            _context.SaveChanges();

        }

        public void RemoveExpense(int cartId, int expid)
        {
            _context.CartExpenses.Remove(_context.CartExpenses.First(x => ((x.CartId == cartId) && (x.CartExpenseId == expid))));
            _context.SaveChanges();
        }
        public void NewCart(string name)
        {
            var cart = new Db.Entities.Cart
            {
                Name = name,
                ClientId = 1
            };

            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

    }
}
