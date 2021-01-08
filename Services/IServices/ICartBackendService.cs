using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface ICartBackendService
    {
        public List<Services.Models.CartInfo> GiveCartNames();
        public List<CartExpense> GiveExpenses(int id);
        public void DeleteCart(int id);
        public void ChargeCart(int id);
        public void ChangeCart(int id, string name, List<CartExpense> cartExpenses);
        public void NewCart(string name);

    }
}
