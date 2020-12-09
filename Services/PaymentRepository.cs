using Plutus.WebService.IRepos;
using System;

namespace Plutus.WebService
{
    class PaymentRepository : IPaymentRepository
    {
        private readonly IFileManagerRepository _fileManager;
        public PaymentRepository(IFileManagerRepository fileManagerRepository) => _fileManager = fileManagerRepository;
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
            _fileManager.AddPayment(payment, chi.CurrentType);
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
            _fileManager.AddPayment(payment, DataType.Expense);
        }
    }
}
