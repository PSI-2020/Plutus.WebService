using System;

namespace Plutus.WebService.IRepos
{
    public interface IGoalsRepository
    {
        public void EditGoal(Goal goal, string newName, string newAmount, DateTime newDueDate);
        public void DeleteGoal(Goal goal);
        public void SetMainGoal(Goal goal);
        public string Insights(Goal goal, string dailyOrMonthly);
    }
}
