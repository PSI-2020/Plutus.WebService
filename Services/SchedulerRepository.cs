using Plutus.WebService.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plutus.WebService
{
    class SchedulerRepository : ISchedulerRepository
    {
        private readonly IFileManagerRepository _fileManager;
        public SchedulerRepository(IFileManagerRepository fileManagerRepository)
        {
            _fileManager = fileManagerRepository;
        }
        public void CheckPayments()
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
        }
        public string ShowPayment(int index, DataType type)
        {
            var list = _fileManager.ReadFromFile<ScheduledPayment>(type);
            if (!list.Any()) return "";

            var date = list[index].Date.ConvertToDate();
            return list[index].Active == false
                ? list[index].Name + " in " + list[index].Category + "\r\n" + "Inactive"
                : list[index].Name + " in " + list[index].Category + "\r\n" + "Next payment: " + date.ToString("yyyy/MM/dd");
        }
        public void ChangeStatus(int index, DataType type, bool status)
        {
            var list = _fileManager.ReadFromFile<ScheduledPayment>(type);
            list[index].Active = status;
            _fileManager.UpdateScheduledPayments(list, type);
        }
        public void DeletePayment(int index, DataType type)
        {
            var list = _fileManager.ReadFromFile<ScheduledPayment>(type);
            list.Remove(list[index]);
            Func<List<ScheduledPayment>, List<ScheduledPayment>> ReID = delegate (List<ScheduledPayment> list)
            {
                list.ForEach(x => x.Id = type.ToString() + list.IndexOf(x));
                return list;
            };
            _fileManager.UpdateScheduledPayments(ReID(list), type);
        }
    }
}
