using Plutus.WebService.IRepos;
using System.Collections.Generic;

namespace Plutus.WebService
{
    public class ShoppingBackendService : IShoppingBackendService
    {
        private readonly IPaymentService _paymentService;
        public ShoppingBackendService(IPaymentService paymentService) => _paymentService = paymentService;
        

        public void ChargeShopping(List<ShoppingExpense> bag)
        {
            for (var i = 0; i < bag.Count; i++)
            {
                if (bag[i].State == 1)
                {
                    _paymentService.AddCartPayment(bag[i].Name, bag[i].Price, bag[i].Category);
                }
            }
        }

    }
}
