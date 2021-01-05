using Db;
using Plutus.WebService.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    public class SchedulerService : ISchedulerService
    {
        private readonly PlutusDbContext _context;
        public SchedulerService(PlutusDbContext context) => _context = context;
        /*public void CheckPayments()
        {
            var incomesList = _fileManager.ReadFromFile<ScheduledPayment>(DataType.MonthlyIncome);
            var expensesList = _fileManager.ReadFromFile<ScheduledPayment>(DataType.MonthlyExpenses);

            for(var x = 0; x < incomesList.Count; x++)
            {
                var date = incomesList[x].Date.ConvertToDate();
                if (DateTime.Now.ConvertToInt() >= incomesList[x].Date && incomesList[x].Active == true)
                {
                    if(incomesList[x].Frequency == "Monthly")
                    {
                        var months = (int)Math.Ceiling((DateTime.Now.ConvertToInt() - incomesList[x].Date) / (30.44 * 24 * 60 * 60));
                        for (var i = 0; i < months; i++)
                        {
                            _fileManager.AddPayment(new Payment(incomesList[x].Date, incomesList[x].Name, incomesList[x].Amount, incomesList[x].Category), DataType.Income);
                        }
                        var newDate = date.AddMonths(months);
                        incomesList[x].Date = newDate.ConvertToInt();
                    }
                    else if(incomesList[x].Frequency == "Weekly")
                    {
                        var weeks = (int)Math.Floor(((double)DateTime.Now.ConvertToInt() - incomesList[x].Date) / (7 * 24 * 60 * 60));
                        for (var i = 0; i < weeks; i++)
                        {
                            _fileManager.AddPayment(new Payment(incomesList[x].Date, incomesList[x].Name, incomesList[x].Amount, incomesList[x].Category), DataType.Income);
                        }
                        var newDate = date.AddDays(weeks * 7);
                        incomesList[x].Date = newDate.ConvertToInt();
                    }
                }
            }
            _fileManager.UpdateScheduledPayments(incomesList, DataType.MonthlyIncome);

            for (var x = 0; x < expensesList.Count; x++)
            {
                var date = expensesList[x].Date.ConvertToDate();
                if (DateTime.Now >= date && expensesList[x].Active == true)
                {
                    if (expensesList[x].Frequency == "Monthly")
                    {
                        var months = (int)Math.Ceiling((DateTime.Now.ConvertToInt() - expensesList[x].Date) / (30.44 * 24 * 60 * 60));
                        for (var i = 0; i < months; i++)
                        {
                            _fileManager.AddPayment(new Payment(expensesList[x].Date, expensesList[x].Name, expensesList[x].Amount, expensesList[x].Category), DataType.Expense);
                        }
                        var newDate = date.AddMonths(months);
                        expensesList[x].Date = newDate.ConvertToInt();
                    }
                    else if (expensesList[x].Frequency == "Weekly")
                    {
                        var weeks = (int)Math.Ceiling(((double)DateTime.Now.ConvertToInt() - expensesList[x].Date) / (7 * 24 * 60 * 60));
                        for (var i = 0; i < weeks; i++)
                        {
                            _fileManager.AddPayment(new Payment(expensesList[x].Date, expensesList[x].Name, expensesList[x].Amount, expensesList[x].Category), DataType.Expense);
                        }
                        var newDate = date.AddDays(weeks * 7);
                        expensesList[x].Date = newDate.ConvertToInt();
                    }
                }
            }
            _fileManager.UpdateScheduledPayments(expensesList, DataType.MonthlyExpenses);
        }*/
        public string ShowPayment(int index, DataType type)
        {
            var list = _context.ScheduledPayments.Where(x => x.PaymentType == (PlutusDb.Entities.DataType)type).ToList();
            if (!list.Any()) return "";
            var payment = list.Where(x => x.ScheduledPaymentId == index).First();

            var date = payment.Date.ConvertToDate();
            return payment.Active == false
                ? list[index].Name + " in " + payment.Category + "\r\n" + "Inactive"
                : list[index].Name + " in " + payment.Category + "\r\n" + "Next payment: " + date.ToString("yyyy/MM/dd");
        }
        public void ChangeStatus(int index, DataType type, bool status)
        {
            var list = _context.ScheduledPayments.Where(x => x.PaymentType == (PlutusDb.Entities.DataType)type).ToList();
            var payment = list.Where(x => x.ScheduledPaymentId == index).First();
            payment.Active = status;
            _context.ScheduledPayments.Update(payment);
            _context.SaveChanges();
        }
        public void AddPayment(ScheduledPayment payment, DataType type)
        {
            var newPayment = new Db.Entities.ScheduledPayment
            {
                Name = payment.Name,
                ClientId = 1,
                Active = payment.Active,
                Amount = payment.Amount,
                PaymentType = (PlutusDb.Entities.DataType)type,
                Date = payment.Date,
                Category = payment.Category,
                Frequency = payment.Frequency
            };
            _context.Add(newPayment);
            _context.SaveChanges();
        }
        public void DeletePayment(int index, DataType type)
        {
            var list =_context.ScheduledPayments.Where(x => x.PaymentType == (PlutusDb.Entities.DataType)type).ToList();
            _context.ScheduledPayments.Remove(list.Where(x => x.ScheduledPaymentId == index).First());
            _context.SaveChanges();
        }
        public void EditScheduledPayment(ScheduledPayment payment, int index, DataType type)
        {
            var list = _context.ScheduledPayments.Where(x => x.PaymentType == (PlutusDb.Entities.DataType)type).ToList();
            var pay = list.Where(x => x.ScheduledPaymentId == index).First();
            pay.Name = payment.Name;
            pay.Active = payment.Active;
            pay.Amount = payment.Amount;
            pay.Date = payment.Date;
            pay.Frequency = payment.Frequency;
            pay.Category = payment.Category;
            _context.ScheduledPayments.Update(pay);
            _context.SaveChanges();
        }

        public List<ScheduledPayment> GetScheduledExpenses()
        {
            var list = _context.ScheduledPayments.Where(x => x.PaymentType == PlutusDb.Entities.DataType.MonthlyExpenses).ToList();
            var result = new List<ScheduledPayment>();
            foreach(var item in list)
            {
                var payment = new ScheduledPayment
                {
                    Date = item.Date,
                    Active = item.Active,
                    Amount = item.Amount,
                    Frequency = item.Frequency,
                    Id = item.ScheduledPaymentId,
                    Name = item.Name
                };
                result.Add(payment);
            }
            return result;
        }
        public List<ScheduledPayment> GetScheduledIncomes()
        {
            var list = _context.ScheduledPayments.Where(x => x.PaymentType == PlutusDb.Entities.DataType.MonthlyIncome).ToList();
            var result = new List<ScheduledPayment>();
            foreach (var item in list)
            {
                var payment = new ScheduledPayment
                {
                    Date = item.Date,
                    Active = item.Active,
                    Amount = item.Amount,
                    Frequency = item.Frequency,
                    Id = item.ScheduledPaymentId,
                    Name = item.Name
                };
                result.Add(payment);
            }
            return result;
        }
    }
}
