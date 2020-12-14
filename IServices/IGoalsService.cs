using System;

namespace Plutus.WebService.IRepos
{
    public interface IGoalsService
    {
        public void EditGoal(int id, Goal newGoal);
        public void DeleteGoal(Goal goal);
        public void SetMainGoal(Goal goal);
        public string Insights(Goal goal, string dailyOrMonthly);
    }
}
