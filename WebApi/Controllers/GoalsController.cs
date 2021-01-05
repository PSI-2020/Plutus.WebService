using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;
using System;
using System.Collections.Generic;

namespace Plutus.WebService
{

    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IGoalsService _goalsService;
        public static event EventHandler<string> GoalDeletedEvent;
        

        public GoalsController(IGoalsService goalsService)
        {
            _goalsService = goalsService;
        }

        private List<Goal> ReadGoals() => _goalsService.GetGoalsList();

        [HttpGet]
        public IEnumerable<Goal> Get() => ReadGoals();

        [HttpGet("{id}/{dailyOrMonthly}")]
        public string GetInsights(int id, string dailyOrMonthly)
        {
            return _goalsService.Insights(id, dailyOrMonthly);
        }

        [HttpPost]
        public void Post([FromBody] Goal goal) => _goalsService.AddGoal(goal);

        [HttpPut]
        public void Put([FromBody] int id) => _goalsService.SetMainGoal(id);

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _goalsService.DeleteGoal(id);
        }

        [HttpPut("edit/{id}")]
        public void EditGoal(int id, [FromBody] Goal newGoal) => _goalsService.EditGoal(id, newGoal);
    }
}
