using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface ICartBackendService
    {
        public List<Services.Models.CartInfo> GiveCartNames();
        public List<CartExpense> GiveExpenses(int id);
        public void DeleteCart(int id);
        public void ChargeCart(int id);
        public void RenameCart(int id, string name);
        public void AddExpense(int cartId, CartExpense expense);
        public void EditExpense(int cartId, CartExpense expense);
        public void RemoveExpense(int cartId, int expenseId);
        public void NewCart(string name);

    }
}
