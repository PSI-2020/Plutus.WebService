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
        private readonly IFileManagerRepository _fileManager;
        private readonly IGoalsRepository _goalsRepository;
        public static event EventHandler<string> GoalDeletedEvent;

        public GoalsController(IFileManagerRepository fileManagerRepository, IGoalsRepository goalsRepository)
        {
            _fileManager = fileManagerRepository;
            _goalsRepository = goalsRepository;
        }

        private List<Goal> ReadGoals() => _fileManager.ReadFromFile<Goal>(DataType.Goals);

        [HttpGet]
        public IEnumerable<Goal> Get() => ReadGoals();

        [HttpGet("{id}/{dailyOrMonthly}")]
        public string GetInsights(int id, string dailyOrMonthly)
        {
            var list = ReadGoals();
            return _goalsRepository.Insights(list[id], dailyOrMonthly);
        }

        [HttpPost]
        public void Post([FromBody] Goal goal)
        {
            _fileManager.AddGoal(goal);
        }

        [HttpPut]
        public void Put([FromBody] Goal goal) => _goalsRepository.SetMainGoal(goal);

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var list = ReadGoals();
            _goalsRepository.DeleteGoal(list[id]);
            GoalDeletedEvent?.Invoke(this, list[id].Name);
        }

        [HttpPut ("edit/{id}")]
        public void EditGoal(int id, Goal newGoal)
        {
            _goalsRepository.EditGoal(id, newGoal);
        }
    }
}
