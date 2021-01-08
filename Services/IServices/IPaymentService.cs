using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface IPaymentService
    {
        public void AddPayment(CurrentInfoHolder chi);
        public void AddPaymentToDatabase(Payment payment, DataType type);
        public void AddCartPayment(string name, double amount, string category);
        public List<Payment> GetPayments();
        public List<Payment> GetPayments(DataType type);
        public void DeletePayment(int id);
        public void EditPayment(Payment payment, int id);
    }
}
