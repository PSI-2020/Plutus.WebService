using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        private List<Budget> ReadBudgets() => _budgetService.GetBudgetsList();

        [HttpGet]
        public string Get()
        {
            var result = "";
            var list = ReadBudgets();
            if (!list.Any()) return "";
            for (var x = 0; x < list.Count; x++)
            {
                result = result + _budgetService.GenerateBudget(x) + "\r\n" + "\r\n";
            }
            return result;
        }
        [HttpGet("list")]
        public List<Budget> GetList() => _budgetService.GetBudgetsList();

        [HttpGet("{id}")]
        public string Get(int id) => _budgetService.GenerateBudget(id);

        [HttpGet("{id}/stats")]
        public List<Payment> GetStats(int id) => _budgetService.ShowStats(id);

        [HttpGet("{id}/spent")]
        public decimal GetSpent(int id) => _budgetService.Spent(id);

        [HttpGet("{id}/left")]
        public decimal GetLeftToSpend(int id) => _budgetService.LeftToSpend(id);

        [HttpPost]
        public void Post([FromBody] Db.Entities.Budget budget) => _budgetService.AddBudget(budget);

        [HttpDelete("{id}")]
        public void Delete(int id) => _budgetService.DeleteBudget(id);
    }
}
