using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface ICartRepository
    {
        public List<string> GiveCarts();
        public List<CartExpense> GiveExpenses(int index);
        public string GiveLoadMessage();
        public void NewCart();
        public void AddExpenseToCart(CurrentInfoHolder cih);
        public int GiveCurrentCartElemCount();
        public CartExpense GiveCurrentElemAt(int i);
        public void RemoveExpenseCurrentAt(int i);
        public void SetCurrentName(string name);
        public string GiveCurrentName();
        public int GiveCartCount();
        public string VerifyName(string name, string prevname);
        public string GiveCartNameAt(int i);
        public void CurrentCartSet(int i);
        public void SaveCartChanges(int i);
        public void DeleteCurrent();
        public void ChargeCart();
        public void SaveCarts();
        public void SaveCarts(int index, string name, List<CartExpense> cartExpenses);
        public List<Cart> LoadCarts();
        public Cart StartShopping();
    }
}
