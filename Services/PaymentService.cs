using System;

namespace Plutus.Services
{
    class PaymentService
    {
        private readonly FileManager _fm;

        public PaymentService(FileManager fm) => _fm = fm;
        public void AddPayment(CurrentInfoHolder chi)
        {
            var date = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var payment = new Payment
            {
                Date = date,
                Name = chi.CurrentName,
                Amount = double.Parse(chi.CurrentAmout),
                Category = chi.CurrentCategory
            };
            _fm.AddPayment(payment, chi.CurrentType);
        }

        public void AddCartPayment(string name, double amount, string category)
        {
            var date = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var payment = new Payment
            {
                Date = date,
                Name = name,
                Amount = amount,
                Category = category
            };
            _fm.AddPayment(payment, "Expense");
        }
    }
}
