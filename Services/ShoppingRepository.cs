using Plutus.WebService.IRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plutus.WebService
{
    public class ShoppingRepository : IShoppingRepository
    {
        private List<ShoppingExpense> _shoppingBag;
        private readonly IFileManagerRepository _fileManager;
        public ShoppingRepository(IFileManagerRepository fileManagerRepository)
        {
            _fileManager = fileManagerRepository;
        }
        
        public void InitializeShoppingService(Cart cart)
        {
            _shoppingBag = new List<ShoppingExpense>();
            for (var i = 0; i < cart.GiveElementC(); i++){
                var expense = new ShoppingExpense(cart.GiveExpense(i), 0);
                _shoppingBag.Add(expense);
            }
        }

        public void ElementClicked(int index)
        {
            if (_shoppingBag[index].State == 0) _shoppingBag[index].State = 1;
            else _shoppingBag[index].State = 0;
        }

        public void ChargeShopping()
        {
            var ps = new PaymentRepository(_fileManager);
            for (var i = 0; i < _shoppingBag.Count; i++)
            {
                if (_shoppingBag[i].State == 1)
                {
                    ps.AddCartPayment(_shoppingBag[i].Name, _shoppingBag[i].Price, _shoppingBag[i].Category);
                }
            }
        }

        public List<string> GiveExpenses(int state)
        {
            var list = new List<string>();
            for(var i = 0; i < _shoppingBag.Count; i++)
            {
                if (_shoppingBag[i].State == state) list.Add(_shoppingBag[i].Name + '|' + i);
            }
            return list;
        }

    }
}
