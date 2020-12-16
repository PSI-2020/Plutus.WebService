using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface IShoppingBackendService
    {
        public void ChargeShopping(List<ShoppingExpense> bag);
    }
}
