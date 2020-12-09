﻿using Plutus.WebService.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly IFileManagerRepository _fileManager;
        public BudgetRepository(IFileManagerRepository fileManagerRepository)
        {
            _fileManager = fileManagerRepository;
        }

        public void DeleteBudget(int index)
        { 
            var list = _fileManager.ReadFromFile<Budget>(DataType.Budgets);
            list.Remove(list[index]);
            Func<List<Budget>, List<Budget>> Rename = delegate (List<Budget> list) { list.ForEach(x => x.Name = "budget" + list.IndexOf(x)); return list; };

            _fileManager.UpdateBudgets(Rename(list));
        }


        public string GenerateBudget(int index)
        {
            var data = "";
            var list = _fileManager.ReadFromFile<Budget>(DataType.Budgets);

            var from = list[index].From.ConvertToDate();
            var to = list[index].To.ConvertToDate();

            var expenses = _fileManager.ReadFromFile<Payment>(DataType.Expense);
            if (!expenses.Any()) return "";


            data = "Budget for " + list[index].Category;
            var total = 0.00;

            total = expenses
                .Where(x => x.Category == list[index].Category)
                .Where(x => x.Date >= list[index].From)
                .Where(x => x.Date <= list[index].To)
                .Sum(x => x.Amount);

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

    }
}