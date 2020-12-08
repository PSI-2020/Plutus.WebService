using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plutus.WebService.IRepos
{
    public interface IBudgetRepository
    {
        string GenerateBudget(int index);
        void DeleteBudget(int index);
        List<Payment> ShowStats(int index);
    }
}
