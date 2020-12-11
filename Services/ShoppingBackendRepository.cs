using Plutus.WebService.IRepos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plutus.WebService
{
    public class ShoppingBackendRepository : IShoppingBackendRepository
    {
        private readonly IPaymentRepository _paymentRepository;
        public ShoppingBackendRepository(IPaymentRepository paymentRepository) => _paymentRepository = paymentRepository;
        

        public void ChargeShopping(List<ShoppingExpense> bag)
        {
            for (var i = 0; i < bag.Count; i++)
            {
                if (bag[i].State == 1)
                {
                    _paymentRepository.AddCartPayment(bag[i].Name, bag[i].Price, bag[i].Category);
                }
            }
        }

    }
}
