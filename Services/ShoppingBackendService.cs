using System;
using System.Collections.Generic;
using System.Text;

namespace Plutus.WebService
{
    public class ShoppingBackendService
    {
        private string _shoppingState;
        public ShoppingBackendService() => _shoppingState = "Initiated";

        public void ChargeShopping(List<ShoppingExpense> bag)
        {
            var ps = new PaymentService(new FileManager());
            for (var i = 0; i < bag.Count; i++)
            {
                if (bag[i].State == 1)
                {
                    ps.AddCartPayment(bag[i].Name, bag[i].Price, bag[i].Category);
                }
            }
            _shoppingState = "complete";
        }

        public string GiveShoppingResult()
        {
            if (_shoppingState == "complete")
            {
                _shoppingState = "reset";
                return "success";
            }
            else
            {
                _shoppingState = "reset";
                return "fail";
            }

        }

    }
}
