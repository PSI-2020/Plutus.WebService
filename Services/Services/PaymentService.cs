﻿using Db;
using Plutus.WebService.IRepos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

        public void AddPaymentToDatabase(Payment payment, DataType type)
        {
            var pay = new Db.Entities.Payment
            {
                Name = payment.Name,
                Amount = payment.Amount,
                Category = payment.Category,
                Date = payment.Date,
                PaymentType = (PlutusDb.Entities.DataType) type,
                ClientId = 1
            };

            _context.Payments.Add(pay);
            _context.SaveChanges();
        }

        public List<Payment> GetPayments()
        {
            var payList = _context.Payments.ToList();
            var list = payList.Select(x => new Payment { PaymentID = x.PaymentId, Date = x.Date, Name = x.Name, Amount = x.Amount, Category = x.Category }).ToList();
            return list;
        }

        public void EditPayment(Payment newPayment, int paymentID) 
        {
            var payment = _context.Payments.First(x => x.PaymentId == paymentID);
            payment.Name = newPayment.Name;
            payment.Amount = newPayment.Amount;
            payment.Category = newPayment.Category;
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }

        public void DeletePayment(int id)
        {
            _context.Payments.Remove(_context.Payments.First(x => x.PaymentId == id));
            _context.SaveChanges();
        }

        public List<Payment> GetPayments(DataType type)
        {
            var payList = _context.Payments.Where(x => x.PaymentType == (PlutusDb.Entities.DataType) type).ToList();
            var list = payList.Select(x => new Payment { PaymentID = x.PaymentId, Date = x.Date, Name = x.Name, Amount = x.Amount, Category = x.Category }).ToList();
            return list;
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
            var pay = new Db.Entities.Payment
            {
                Name = payment.Name,
                Amount = payment.Amount,
                Category = payment.Category,
                Date = payment.Date,
                PaymentType = (PlutusDb.Entities.DataType)DataType.Expense,
                ClientId = 1
            };

            _context.Payments.Add(pay);
            _context.SaveChanges();
        }
    }
}
