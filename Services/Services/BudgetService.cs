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
        private readonly SmartsaverDbContext _context;
        public BudgetService(IFileManagerRepository fileManagerRepository, SmartsaverDbContext context)
        {
            _fileManager = fileManagerRepository;
            _context = context;
        }

        public void DeleteBudget(int index)
        { 
            var list = _fileManager.ReadFromFile<Budget>(DataType.Budgets);
            list.Remove(list[index]);
            Func<List<Budget>, List<Budget>> Rename = delegate (List<Budget> list) { list.ForEach(x => x.Name = "budget" + list.IndexOf(x)); return list; };

            _fileManager.UpdateBudgets(Rename(list));
        }

        public void AddBudget(Db.Entities.Budget budget)
        {
            var b = new Db.Entities.Budget();
            b.Amount = 500;
            b.Category = "Groceries";
            b.ClientId = 1;
            b.From = 1598918400;
            b.To = 1607990400;
            _context.Budgets.Add(b);
            _context.SaveChanges();
        }


        public string GenerateBudget(int index)
        {
            var list = _fileManager.ReadFromFile<Budget>(DataType.Budgets);

            var from = list[index].From.ConvertToDate();
            var to = list[index].To.ConvertToDate();

            var data = "Budget for " + list[index].Category;

            var total = Spent(index);
            if (total == 0) return "";

            data += "\r\n" + total + "/" + list[index].Sum + " €" + "\r\n" + Math.Round(total * 100 / list[index].Sum, 2) + "%" + "\r\n" +
                from.ToString("yyyy/MM/dd") + " - " + to.ToString("yyyy/MM/dd");

            return data;
        }
        public List<Payment> ShowStats(int index)
        {
            var budgets = _fileManager.ReadFromFile<Budget>(DataType.Budgets);
            var expenses = _fileManager.ReadFromFile<Payment>(DataType.Expense);

            var list =
                (from exp in expenses
                 where exp.Category == budgets[index].Category
                 where exp.Date >= budgets[index].From
                 where exp.Date <= budgets[index].To
                 select exp).ToList();
            return !list.Any() ? null : list;
        }
        public double Spent(int index)
        {
            var list = _fileManager.ReadFromFile<Budget>(DataType.Budgets);

            var expenses = _fileManager.ReadFromFile<Payment>(DataType.Expense);
            if (!expenses.Any()) return 0.00;

            var total = 0.00;

            total = expenses
                .Where(x => x.Category == list[index].Category)
                .Where(x => x.Date >= list[index].From)
                .Where(x => x.Date <= list[index].To)
                .Sum(x => x.Amount);
            return total;
        }
        public double LeftToSpend(int index)
        {
            var list = _fileManager.ReadFromFile<Budget>(DataType.Budgets);
            return list[index].Sum - Spent(index);
        }

    }
}
