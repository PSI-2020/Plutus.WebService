using Db;
using Plutus.WebService.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    public class GoalsService : IGoalsService
    {
        private readonly PlutusDbContext _context;
        public GoalsService(PlutusDbContext context)
        {
            _context = context;
        }

        public List<Goal> GetGoalsList()
        {
            var list = _context.Goals.ToList();
            var goals = new List<Goal>();
            foreach(var item in list)
            {
                var g = new Goal
                { 
                    Name = item.Name,
                    Amount = item.Amount,
                    DueDate = item.DueDate,
                    Id = item.GoalId
                };
                goals.Add(g);
            }
            return goals;
        }
        public void AddGoal(Goal goal)
        {
            var g = new Db.Entities.Goal
            {
                Name = goal.Name,
                DueDate = goal.DueDate,
                Amount = goal.Amount,
                ClientId = 1
            };
            _context.Goals.Add(g);
            _context.SaveChanges();
        }
        public void EditGoal(int id, Goal newGoal)
        {
            var goal = _context.Goals.Where(x => x.GoalId == id).First();
            goal.Name = newGoal.Name;
            goal.DueDate = newGoal.DueDate;
            goal.Amount = newGoal.Amount;
            goal.ClientId = 1;
            _context.Goals.Update(goal);
            _context.SaveChanges();
        }
       
        public void DeleteGoal(int id)
        {
            var g = _context.Goals.Where(x => x.GoalId == id).First();
            _context.Goals.Remove(g);
            _context.SaveChanges();
        }


        public void SetMainGoal(int id)
        {
            var goal = _context.Goals.Where(x => x.GoalId == id).First();
            var ng = new Db.Entities.Goal
            {
                Name = goal.Name,
                DueDate = goal.DueDate,
                Amount = goal.Amount,
                ClientId = 1
            };
            DeleteGoal(id);
            _context.Goals.Add(ng);
            _context.SaveChanges();
        }

        public string Insights(int id, string dailyOrMonthly)
        {
            var scheduledIncome = _context.ScheduledPayments.Where(x => x.PaymentType == PlutusDb.Entities.DataType.MonthlyIncome).ToList();
            var scheduledExpenses = _context.ScheduledPayments.Where(x => x.PaymentType == PlutusDb.Entities.DataType.MonthlyExpenses).ToList();
            var allIncome = _context.Payments.Where(x => x.PaymentType == PlutusDb.Entities.DataType.Income).ToList();
            var allExpenses = _context.Payments.Where(x => x.PaymentType == PlutusDb.Entities.DataType.Expense).ToList();
            var goal = _context.Goals.Where(x => x.GoalId == id).First();

            var months = goal.DueDate.Month - DateTime.Now.Month + (12 * (goal.DueDate.Year - DateTime.Now.Year));
            var weeks = (int)(goal.DueDate - DateTime.Now).TotalDays / 7;

            var weeklyIncome = scheduledIncome.Where(x => x.Active == true).Where(x => x.Frequency == "Weekly").Sum(x => x.Amount * weeks);
            var weeklyExpenses = scheduledExpenses.Where(x => x.Active == true).Where(x => x.Frequency == "Weekly").Sum(x => x.Amount * weeks);
            var monthlyIncome = scheduledIncome.Where(x => x.Active == true).Where(x => x.Frequency == "Monthly").Sum(x => x.Amount * months);
            var monthlyExpenses = scheduledExpenses.Where(x => x.Active == true).Where(x => x.Frequency == "Monthly").Sum(x => x.Amount * months);

            var income = weeklyIncome + monthlyIncome + allIncome.Sum(x => x.Amount);
            var expenses = weeklyExpenses + monthlyExpenses + allExpenses.Sum(x => x.Amount);

            var todaySpent = allExpenses.Where(x => x.Date.ConvertToDate().ToString("yyyy/MM/dd") == DateTime.Now.ToString("yyyy/MM/dd")).Sum(x => x.Amount);

            var monthly = (income - expenses - goal.Amount + todaySpent) / (months + 1);

            return dailyOrMonthly.ToLower() switch
            {
                "monthly" => monthly.ToString("C2"),
                "daily" => ((monthly / DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) - todaySpent).ToString("C2"),
                _ => "",
            };
        }
    }
}
