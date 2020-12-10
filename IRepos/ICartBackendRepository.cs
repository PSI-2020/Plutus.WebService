using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface ICartBackendRepository
    {
        public List<string> GiveCartNames();
        public List<CartExpense> GiveExpenses(int index);
        public void DeleteCart(int index);
        public void ChargeCart(int index);
        public void SaveCarts(int index, string name, List<CartExpense> cartExpenses);
    }
}
