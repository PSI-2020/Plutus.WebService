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
        public void CheckPayments()
        {
            var incomesList = _context.ScheduledPayments.Where(x => (DataType)x.PaymentType == DataType.MonthlyIncome).ToList();
            var expensesList = _context.ScheduledPayments.Where(x => (DataType)x.PaymentType == DataType.MonthlyExpenses).ToList();

            RunCycle(incomesList, DataType.Income);
            RunCycle(expensesList, DataType.Expense);
        }

        public void RunCycle(List<Db.Entities.ScheduledPayment> list, DataType type)
        {
            for (var x = 0; x < list.Count; x++)
            {
                var date = list[x].Date.ConvertToDate();
                if (DateTime.Now.ConvertToInt() >= list[x].Date && list[x].Active == true)
                {
                    if (list[x].Frequency == "Monthly")
                    {
                        var months = (int)Math.Ceiling((DateTime.Now.ConvertToInt() - list[x].Date) / (30.44 * 24 * 60 * 60));
                        for (var i = 0; i < months; i++)
                        {
                            _context.Payments.Add(new Db.Entities.Payment
                            {
                                Amount = list[x].Amount,
                                Category = list[x].Category,
                                ClientId = 1,
                                Date = list[x].Date,
                                Name = list[x].Name,
                                PaymentType = (PlutusDb.Entities.DataType)type
                            });
                        }
                        var newDate = date.AddMonths(months);
                        list[x].Date = newDate.ConvertToInt();
                        _context.ScheduledPayments.Update(list[x]);
                        _context.SaveChanges();
                    }
                    else if (list[x].Frequency == "Weekly")
                    {
                        var weeks = (int)Math.Floor(((double)DateTime.Now.ConvertToInt() - list[x].Date) / (7 * 24 * 60 * 60));
                        for (var i = 0; i < weeks; i++)
                        {
                            _context.Payments.Add(new Db.Entities.Payment
                            {
                                Amount = list[x].Amount,
                                Category = list[x].Category,
                                ClientId = 1,
                                Date = list[x].Date,
                                Name = list[x].Name,
                                PaymentType = (PlutusDb.Entities.DataType)type
                            });
                        }
                        var newDate = date.AddDays(weeks * 7);
                        list[x].Date = newDate.ConvertToInt();
                        _context.ScheduledPayments.Update(list[x]);
                        _context.SaveChanges();
                    }
                }
            }
        }
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
