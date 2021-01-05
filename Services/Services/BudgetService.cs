using Db;
using Plutus.WebService.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    public class BudgetService : IBudgetService
    {
        private readonly IFileManagerRepository _fileManager;
        private readonly PlutusDbContext _context;
        public BudgetService(IFileManagerRepository fileManagerRepository, PlutusDbContext context)
        {
            _fileManager = fileManagerRepository;
            _context = context;
        }

        public List<Budget> GetBudgetsList()
        {
            var list = _context.Budgets.ToList();
            var budgets = new List<Budget>();
            foreach(var bud in list)
            {
                var budget = new Budget
                {
                    Name = "budget" + bud.BudgetId,
                    Category = bud.Category,
                    Sum = bud.Amount,
                    From = bud.From,
                    To = bud.To
                };
                budgets.Add(budget);
            }
            return budgets;
        }

        public void DeleteBudget(int index)
        { 
            var budget = _context.Budgets.Where(x => x.BudgetId == index).First();
            _context.Budgets.Remove(budget);
            _context.SaveChanges();
        }

        public void AddBudget(Budget budget)
        {
            var b = new Db.Entities.Budget
            {
                Amount = budget.Sum,
                Category = budget.Category,
                ClientId = 1,
                From = budget.From,
                To = budget.To
            };
            _context.Budgets.Add(b);
            _context.SaveChanges();
        }


        public string GenerateBudget(int index)
        {
            var list = _context.Budgets.ToList();
            var budget = list.Where(x => x.BudgetId == index).First();

            var from = budget.From.ConvertToDate();
            var to = budget.To.ConvertToDate();

            var data = "Budget for " + budget.Category;

            var total = Spent(index);

            data += "\r\n" + total + "/" + budget.Amount + " €" + "\r\n" + Math.Round(total * 100 / budget.Amount, 2) + "%" + "\r\n" +
                from.ToString("yyyy/MM/dd") + " - " + to.ToString("yyyy/MM/dd");

            return data;
        }
        public List<Payment> ShowStats(int index)
        {
            var expenses = _fileManager.ReadFromFile<Payment>(DataType.Expense);
            var budgets = _context.Budgets.ToList();

            var list =
                (from exp in expenses
                 where exp.Category == budgets[index].Category
                 where exp.Date >= budgets[index].From
                 where exp.Date <= budgets[index].To
                 select exp).ToList();
            return !list.Any() ? null : list;
        }
        public decimal Spent(int index)
        {
            var budgets = _context.Budgets.ToList();
            var budget = budgets.Where(x => x.BudgetId == index).First();

            var expenses = _fileManager.ReadFromFile<Payment>(DataType.Expense);
            if (!expenses.Any()) return 0.00m;

            var total = 0.00;

            total = expenses
                .Where(x => x.Category == budget.Category)
                .Where(x => x.Date >= budget.From)
                .Where(x => x.Date <= budget.To)
                .Sum(x => x.Amount);
            return (decimal)total;
        }
        public decimal LeftToSpend(int index)
        {
            var list = _context.Budgets.ToList();
            var budget = list.Where(x => x.BudgetId == index).First();
            return budget.Amount - Spent(index);
        }

    }
}
