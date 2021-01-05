using System;
using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface IGoalsService
    {
        public void EditGoal(int id, Goal newGoal);
        public void DeleteGoal(int id);
        public void SetMainGoal(int id);
        public string Insights(int id, string dailyOrMonthly);
        public List<Goal> GetGoalsList();
        public void AddGoal(Goal goal);
    }
}
