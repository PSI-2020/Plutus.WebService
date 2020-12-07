using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Plutus.WebService
{

    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly FileManager _fileManager = new FileManager();
        private readonly GoalService _goalService = new GoalService();
        public static event EventHandler<string> GoalDeletedEvent;

        private List<Goal> ReadGoals() => _fileManager.ReadFromFile<Goal>(DataType.Goals);

        // GET: api/<GoalsController>
        [HttpGet]
        public IEnumerable<Goal> Get() => ReadGoals();

        // GET api/<GoalsController>/5
        [HttpGet("{name}/{dailyOrMonthly}")]
        public string GetInsights(string name, string dailyOrMonthly)
        {
            var list = ReadGoals();
            var item = list.Where(x => x.Name == name).ToList();
            return _goalService.Insights(item[0], dailyOrMonthly);
        }

        // POST api/<GoalsController>
        [HttpPost]
        public void Post([FromBody] Goal goal)
        {
            _fileManager.AddGoal(goal);
        }

        // PUT api/<GoalsController>/
        [HttpPut]
        public void Put([FromBody] Goal goal) => _goalService.SetMainGoal(goal);

        // DELETE api/<GoalsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var list = ReadGoals();
            _goalService.DeleteGoal(list[id]);
            GoalDeletedEvent?.Invoke(this, list[id].Name);
        }
    }
}
