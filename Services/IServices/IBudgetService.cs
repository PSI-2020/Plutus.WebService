using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface IBudgetService
    {
        string GenerateBudget(int index);
        void DeleteBudget(int index);
        List<Payment> ShowStats(int index);
        double Spent(int index);
        double LeftToSpend(int index);
    }
}
