using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface IShoppingBackendRepository
    {
        public void ChargeShopping(List<ShoppingExpense> bag);
    }
}
