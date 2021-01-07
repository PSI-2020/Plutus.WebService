using Db;
using Microsoft.Data.SqlClient;
using Plutus.WebService.IRepos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace Plutus.WebService
{
    public class BudgetService : IBudgetService
    {
        private readonly PlutusDbContext _context;
        private readonly ILoggerService _logger;
        public BudgetService(PlutusDbContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Budget> GetBudgetsList()
        {
            /*var list = _context.Budgets.ToList();
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
            return budgets;*/
            var list = new List<Budget>();
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = "Server=tcp:plutus-psi.database.windows.net,1433;Initial Catalog=Plutus;Persist Security Info=False;User ID=plutus;Password=2s2E9SRCC@4q;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Budgets", cn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Budget");

                    list = ds.Tables[0].AsEnumerable().Select(x => new Budget
                    {
                        Name = "budget" + x.Field<int>("BudgetId"),
                        Category = x.Field<string>("Category"),
                        Sum = x.Field<decimal>("Amount"),
                        From = x.Field<int>("From"),
                        To = x.Field<int>("To")
                    }).ToList();


                    da.Dispose();
                }
                catch(SqlException ex)
                {
                    _logger.Log("SQLException:" + ex.ToString());
                }
                catch(Exception e)
                {
                    _logger.Log("Exception:" + e.ToString());
                }
                finally
                {
                    cn.Close();
                }
            }
            return list;
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
            var list = _context.Payments.Where(x => x.PaymentType == PlutusDb.Entities.DataType.Expense).ToList();
            var expenses = new List<Payment>();
            foreach(var item in list)
            {
                var payment = new Payment
                {
                    Name = item.Name,
                    Amount = item.Amount,
                    Date = item.Date,
                    Category = item.Category
                };
                expenses.Add(payment);
            }
            var budgets = _context.Budgets.ToList();
            var budget = budgets.Where(x => x.BudgetId == index).First();

            var result =
                (from exp in expenses
                 where exp.Category == budget.Category
                 where exp.Date >= budget.From
                 where exp.Date <= budget.To
                 select exp).ToList();
            return !result.Any() ? null : result;
        }
        public decimal Spent(int index)
        {
            var budgets = _context.Budgets.ToList();
            var budget = budgets.Where(x => x.BudgetId == index).First();

            var expenses = _context.Payments.Where(x => (DataType)x.PaymentType == DataType.Expense).ToList();
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
