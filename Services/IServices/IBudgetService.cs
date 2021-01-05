using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface IBudgetService
    {
        string GenerateBudget(int index);
        void DeleteBudget(int index);
        List<Payment> ShowStats(int index);
        decimal Spent(int index);
        decimal LeftToSpend(int index);
        void AddBudget(Db.Entities.Budget budget);
        List<Budget> GetBudgetsList();
    }
}
