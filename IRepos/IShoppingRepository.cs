using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface IShoppingRepository
    {
        public void InitializeShoppingService(Cart cart);
        public void ElementClicked(int index);
        public void ChargeShopping();
        public List<string> GiveExpenses(int state);
    }
}
