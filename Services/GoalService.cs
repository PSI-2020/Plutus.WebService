﻿using System;
using System.Linq;

namespace Plutus.WebService
{
    public class GoalService
    {
        private readonly FileManager _fileManager = new FileManager();
        public void EditGoal(Goal goal, string newName, string newAmount, DateTime newDueDate)
        {
            var amount = double.Parse(newAmount);
            var list = _fileManager.ReadGoals();
            var index = list.IndexOf(list.First(i => goal.Name == i.Name && goal.Amount == i.Amount && goal.DueDate == i.DueDate));
            list[index] = new Goal(newName, amount, newDueDate);
            _fileManager.UpdateGoals(list);

        }

        public void DeleteGoal(Goal goal)
        {
            var list = _fileManager.ReadGoals();
            list.Remove(list.First(i => goal.Name == i.Name && goal.Amount == i.Amount && goal.DueDate == i.DueDate));
            _fileManager.UpdateGoals(list);
        }

        public void SetMainGoal(Goal goal)
        {
            DeleteGoal(goal);
            var list = _fileManager.ReadGoals();
            list.Insert(0, goal);
            _fileManager.UpdateGoals(list);
        }

        public string Insights(Goal goal, string dailyOrMonthly)
        {
            var scheduledIncome = _fileManager.LoadScheduledPayments(DataType.MonthlyIncome);
            var scheduledExpenses = _fileManager.LoadScheduledPayments(DataType.MonthlyExpenses);
            var allIncome = _fileManager.ReadPayments(DataType.Income);
            var allExpenses = _fileManager.ReadPayments(DataType.Expense);

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
