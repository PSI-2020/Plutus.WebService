using Db;
using Plutus.WebService.IRepos;
using System;

namespace Plutus.WebService
{
    public class PaymentService : IPaymentService
    {
        private readonly IFileManagerRepository _fileManager;
        private readonly PlutusDbContext _context;

        public PaymentService(IFileManagerRepository fileManagerRepository, PlutusDbContext context)
        {
            _fileManager = fileManagerRepository;
            _context = context;

        }
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

        public void AddPaymentToDatabase(Db.Entities.Expense payment, DataType type)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
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
