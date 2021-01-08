using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private readonly ISchedulerService _schedulerService;
        public SchedulerController(ISchedulerService schedulerService) => _schedulerService = schedulerService;
        private List<ScheduledPayment> ReadExpenses() => _schedulerService.GetScheduledExpenses();
        private List<ScheduledPayment> ReadIncomes() => _schedulerService.GetScheduledIncomes();


        [HttpGet("{type}")]
        public List<ScheduledPayment> Get(DataType type) => type == DataType.MonthlyExpenses ? ReadExpenses() : type == DataType.MonthlyIncome ? ReadIncomes() : null;

        [HttpGet("{id}/{type}")]
        public string Get(int id, DataType type) => _schedulerService.ShowPayment(id, type);

        [HttpPost("{type}")]
        public void Post([FromBody] ScheduledPayment payment, DataType type) => _schedulerService.AddPayment(payment, type);

        [HttpPut("{id}/{type}/{status}")]
        public void Put(int id, DataType type, bool status) => _schedulerService.ChangeStatus(id, type, status);

        [HttpPut("edit/{id}/{type}")]
        public void Put([FromBody] ScheduledPayment payment, int id, DataType type) => _schedulerService.EditScheduledPayment(payment, id, type);

        [HttpDelete("{id}/{type}")]
        public void Delete(int id, DataType type) => _schedulerService.DeletePayment(id, type);

        [HttpPatch]
        public void CheckPayments() => _schedulerService.CheckPayments();
    }
}
