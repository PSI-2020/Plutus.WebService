using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Plutus.Services
{
    public class HistoryService
    {
        DataTable ListToDataTable(List<Payment> list)
        {
            var table = new DataTable();

            table.Columns.Add("Date");
            table.Columns.Add("Name");
            table.Columns.Add("Amount");
            table.Columns.Add("Category");

            list.OrderByDescending(x => x.Date).ToList().ForEach(x => table.Rows.Add(x.Date.ConvertToDate(), x.Name, x.Amount, x.Category));

            return !list.Any() ? null : table;
        }

        DataTable ListToDataTable(List<Payment> expenseList, List<Payment> incomeList)
        {
            var table = new DataTable();

            table.Columns.Add("Date");
            table.Columns.Add("Name");
            table.Columns.Add("Amount");
            table.Columns.Add("Category");
            table.Columns.Add("Type");

            var list = expenseList.Select(x => new { Date = x.Date.ConvertToDate(), x.Name, x.Amount, x.Category, Type = "Expense" })
                                      .ToList();
            list.AddRange(incomeList.Select(x => new { Date = x.Date.ConvertToDate(), x.Name, x.Amount, x.Category, Type = "Income" })
                                                  .ToList());

            list.OrderByDescending(x => x.Date).ToList().ForEach(x => table.Rows.Add(x.Date, x.Name, x.Amount, x.Category, x.Type));
            return !list.Any() ? null : table;
        }

        public DataTable LoadDataGrid(FileManager fileManager, int index)
        {
            return index switch
            {
                0 => ListToDataTable(fileManager.ReadPayments("Expense"), fileManager.ReadPayments("Income")),
                1 => ListToDataTable(fileManager.ReadPayments("Expense")),
                2 => ListToDataTable(fileManager.ReadPayments("Income")),
                _ => null,
            };
        }

        //public DataTable LoadDataGrid(FileManager fileManager, int index)
        //{
        //    return index switch
        //    {
        //        0 => ListToDataTable(fileManager.ReadPayments("Expense"), fileManager.ReadPayments("Income")),
        //        1 => ListToDataTable(fileManager.ReadPayments("Expense")),
        //        2 => ListToDataTable(fileManager.ReadPayments("Income")),
        //        _ => null,
        //    };
        //}

    }
}
