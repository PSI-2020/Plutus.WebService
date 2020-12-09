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
        private readonly IFileManagerRepository _fileManager;
        private readonly IBudgetRepository _budgetRepository;

        // Inject IEmployeeRepository using Constructor Injection
        public BudgetsController(IBudgetRepository budgetRepository, IFileManagerRepository fileManagerRepository)
        {
            _budgetRepository = budgetRepository;
            _fileManager = fileManagerRepository;
        }

        private List<Budget> ReadBudgets() => _fileManager.ReadFromFile<Budget>(DataType.Budgets);

        [HttpGet]
        public ActionResult<string> Get()
        {
            var result = "";
            var list = ReadBudgets();
            if (!list.Any()) return "";
            for (var x = 0; x < list.Count; x++)
            {
                result = result + _budgetRepository.GenerateBudget(x) + "\r\n" + "\r\n";
            }
            return result;
        }
        [HttpGet("list")]
        public List<Budget> GetList()
        {
            return ReadBudgets();
        }

        [HttpGet("{id}")]
        public string Get(int id) => _budgetRepository.GenerateBudget(id);

        [HttpGet("{id}/stats")]
        public List<Payment> GetStats(int id) => _budgetRepository.ShowStats(id);

        [HttpPost]
        public void Post([FromBody] Budget budget) => _fileManager.AddBudget(budget);

        [HttpDelete("{id}")]
        public void Delete(int id) => _budgetRepository.DeleteBudget(id);
    }
}
