using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IFileManagerRepository _fileManager;
        public delegate void PaymentAddedHandler(Payment payment);
        public static event PaymentAddedHandler PaymentAdded;

        public PaymentController(IFileManagerRepository fileManagerRepository) => _fileManager = fileManagerRepository;

        [HttpGet]
        public List<Payment> Get()
        {
            var list = _fileManager.ReadFromFile<Payment>(DataType.Expense);
            list.AddRange(_fileManager.ReadFromFile<Payment>(DataType.Income));

            return list;
        }
        [HttpGet("{type}")]
        public List<Payment> Get(DataType type) => _fileManager.ReadFromFile<Payment>(type);


        [HttpPost("{type}")]
        public Payment Post(Payment payment, DataType type)
        {
            if (Enum.IsDefined(typeof(DataType), type))
            {
                _fileManager.AddPayment(payment, type);
                PaymentAdded?.Invoke(payment);
                return payment;
            }
            else
            {
                return null;
            }
        }

        [HttpPut("{type}/{index}")]
        public void Edit(Payment payment, int index, DataType type) => _fileManager.EditPayment(payment, index, type);

        [HttpPut("{type}")]
        public void Delete(Payment payment, DataType type) => _fileManager.DeletePayment(payment, type);
    }
}