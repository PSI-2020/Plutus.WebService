using System.Collections.Generic;
using System.Xml.Linq;

namespace Plutus.WebService.IRepos
{
    public interface IFileManagerRepository
    {
        public List<T> ReadFromFile<T>(DataType type) where T : class;
        public void EditPayment(Payment payment, Payment newPayment, DataType type);
        public void AddPayment(Payment payment, DataType type);
        public void AddGoal(Goal goal);
        public void UpdateGoals(List<Goal> list);
        public void AddBudget(Budget budget);
        public void UpdateBudgets(List<Budget> list);
        public void AddScheduledPayment(ScheduledPayment payment, DataType type);
        public void UpdateScheduledPayments(List<ScheduledPayment> list, DataType type);
        public void SaveCarts(XElement carts);
        public XElement LoadCarts();
    }
}
