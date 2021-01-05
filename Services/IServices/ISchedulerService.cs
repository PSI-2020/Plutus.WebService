using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface ISchedulerService
    {
        //public void CheckPayments();
        public string ShowPayment(int index, DataType type);
        public void ChangeStatus(int index, DataType type, bool status);
        public void DeletePayment(int index, DataType type);
        public void EditScheduledPayment(ScheduledPayment payment, int index, DataType type);
        public List<ScheduledPayment> GetScheduledIncomes();
        public List<ScheduledPayment> GetScheduledExpenses();
        public void AddPayment(ScheduledPayment payment, DataType type);
    }
}
