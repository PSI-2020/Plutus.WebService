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
        private readonly IPaymentService _paymentService;
        public delegate void PaymentAddedHandler(Payment payment);
        public static event PaymentAddedHandler PaymentAdded;

        public PaymentController(IPaymentService paymentService) 
        {
            _paymentService = paymentService;
         }

        [HttpGet]
        public List<Payment> Get() => _paymentService.GetPayments();

        [HttpGet("{type}")]
        public List<Payment> Get(DataType type) => _paymentService.GetPayments(type);


        [HttpPost("{type}")]
        public Payment Post([FromBody] Payment payment, DataType type)
        {
            if (Enum.IsDefined(typeof(DataType), type))
            {
                _paymentService.AddPaymentToDatabase(payment, type);
                PaymentAdded?.Invoke(payment);
                return payment;
            }
            else
            {
                return null;
            }
        }

        [HttpPut("{type}/{index}")]
        public void Edit(Payment payment, int index, DataType type) => _paymentService.EditPayment();

        [HttpPut("{type}")]
        public void Delete(Payment payment, DataType type) => _paymentService.DeletePayment(payment, type);
    }
}