using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plutus.WebService
{
    class SchedulerService
    {
        private readonly FileManager _fileManager = new FileManager();
        public void CheckPayments()
        {
            var incomesList = _fileManager.LoadScheduledPayments(DataType.MonthlyIncome);
            var expensesList = _fileManager.LoadScheduledPayments(DataType.MonthlyExpenses);

            for(var x = 0; x < incomesList.Count; x++)
            {
                var date = incomesList[x].Date.ConvertToDate();
                if (DateTime.Now >= date && incomesList[x].Active == true)
                {
                    _fileManager.AddPayment(new Payment(incomesList[x].Date, incomesList[x].Name, incomesList[x].Amount, incomesList[x].Category), DataType.Income);
                    if(incomesList[x].Frequency == "Monthly")
                    {
                        var newDate = date.AddMonths(1);
                        incomesList[x].Date = (int)newDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    }
                    else if(incomesList[x].Frequency == "Weekly")
                    {
                        var newDate = date.AddDays(7);
                        incomesList[x].Date = (int)newDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    }
                }
            }

            for (var x = 0; x < expensesList.Count; x++)
            {
                var date = expensesList[x].Date.ConvertToDate();
                if (DateTime.Now >= date && expensesList[x].Active == true)
                {
                    _fileManager.AddPayment(new Payment(expensesList[x].Date, expensesList[x].Name, expensesList[x].Amount, expensesList[x].Category), DataType.Expense);
                    if (expensesList[x].Frequency == "Monthly")
                    {
                        var newDate = date.AddMonths(1);
                        expensesList[x].Date = (int)newDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    }
                    else if (expensesList[x].Frequency == "Weekly")
                    {
                        var newDate = date.AddDays(7);
                        expensesList[x].Date = (int)newDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    }
                }
            }
            _fileManager.UpdateScheduledPayments(expensesList, DataType.MonthlyExpenses);
        }
        public string ShowPayment(int index, DataType type)
        {
            var list = _fileManager.LoadScheduledPayments(type);
            if (!list.Any()) return "";

            var date = list[index].Date.ConvertToDate();
            return list[index].Active == false
                ? list[index].Name + " in " + list[index].Category + "\r\n" + "Inactive"
                : list[index].Name + " in " + list[index].Category + "\r\n" + "Next payment: " + date.ToString("yyyy/MM/dd");
        }
        public void ChangeStatus(int index, DataType type, bool status)
        {
            var list = _fileManager.LoadScheduledPayments(type);
            list[index].Active = status;
            _fileManager.UpdateScheduledPayments(list, type);
        }
        public void DeletePayment(int index, DataType type)
        {
            var list = _fileManager.LoadScheduledPayments(type);
            list.Remove(list[index]);
            _fileManager.UpdateScheduledPayments(ReIDPayments(list, type), type);
        }
        public List<ScheduledPayment> ReIDPayments(List<ScheduledPayment> list, DataType type)
        {
            list.ForEach(x => x.Id = type.ToString() + list.IndexOf(x));
            return list;
        }
    }
}
