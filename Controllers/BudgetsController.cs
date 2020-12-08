using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly FileManager _fileManager = new FileManager();
        private readonly BudgetService _budgetService = new BudgetService();

        private List<Budget> ReadBudgets() => _fileManager.ReadFromFile<Budget>(DataType.Budgets);

        [HttpGet]
        public ActionResult<string> Get()
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
        public List<Budget> GetList()
        {
            return ReadBudgets();
        }

        [HttpGet("{id}")]
        public string Get(int id) => _budgetService.GenerateBudget(id);

        [HttpGet("{id}/stats")]
        public List<Payment> GetStats(int id) => _budgetService.ShowStats(id);

        [HttpPost]
        public void Post([FromBody] Budget budget) => _fileManager.AddBudget(budget);

        [HttpDelete("{id}")]
        public void Delete(int id) => _budgetService.DeleteBudget(id);
    }
}
