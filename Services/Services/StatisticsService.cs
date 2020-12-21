using Plutus.WebService.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    enum ExpenseCategories
    {
        Groceries,
        Restaurant,
        Clothes,
        Transport,
        Health,
        School,
        Bills,
        Entertainment,
        Other
    }

    enum IncomeCategories
    {
        Salary,
        Gift,
        Investment,
        Sale,
        Rent,
        Other
    }

    public class StatisticsService : IStatisticsService
    {
        private readonly IFileManagerRepository _fileManager;

        public StatisticsService(IFileManagerRepository fileManager) => _fileManager = fileManager; 

        public string GenerateExpenseStatistics()
        {
            var list = _fileManager.ReadFromFile<Payment>(DataType.Expense);
            if (!list.Any()) return "No expense data found!";

            var data = "Expense statistics:\r\n\r\n";
            var total = list.Sum(x => x.Amount);
            var sums = new Dictionary<string, double>();

            foreach (var category in Enum.GetNames(typeof(ExpenseCategories)))
            {
                sums.Add(category, list.Where(x => x.Category == category).Sum(x => x.Amount));
            }

            foreach (var category in Enum.GetNames(typeof(ExpenseCategories)))
            {
                var percent = total == 0
                    ? " (" + string.Format("{0:0.00}", 0) + "%)"
                    : " (" + string.Format("{0:0.00}", sums[category] / total * 100) + "%)";
                data += category + " " + string.Format("{0:0.00}", sums[category]) + percent + "\r\n";
            }
            return data;
        }

        public string GenerateIncomeStatistics()
        {
            var list = _fileManager.ReadFromFile<Payment>(DataType.Income);
            if (!list.Any()) return "No income data found!";

            var data = "Income statistics: \r\n\r\n";
            var total = list.Sum(x => x.Amount);
            var sums = new Dictionary<string, double>();

            foreach (var category in Enum.GetNames(typeof(IncomeCategories)))
            {
                sums.Add(category, list.Where(x => x.Category == category).Sum(x => x.Amount));
            }

            foreach (var category in Enum.GetNames(typeof(IncomeCategories)))
            {
                var percent = total == 0
                    ? " (" + string.Format("{0:0.00}", 0) + "%)"
                    : " (" + string.Format("{0:0.00}", sums[category] / total * 100) + "%)";
                data += category + " " + string.Format("{0:0.00}", sums[category]) + percent + "\r\n";
            }
            return data;
        }
        public decimal CategorySum(string category, DataType type)
        {
            var sum = 0.00;
            var list = _fileManager.ReadFromFile<Payment>(type);
            if (!list.Any()) return (decimal)sum;

            sum = list.Where(x => x.Category == category).Sum(x => x.Amount);

            return (decimal)sum;
        }
    }
}
