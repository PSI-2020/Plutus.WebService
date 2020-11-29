using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Plutus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly FileManager _fileManager = new FileManager();

        [HttpGet]
        public ActionResult<List<Payment>> Get()
        {
            var list = _fileManager.ReadPayments("Expense");
            list.AddRange(_fileManager.ReadPayments("Income"));

            list.Add(new Payment
            {
                Date = DateTime.UtcNow.ConvertToInt(),
                Name = "Test",
                Amount = 69,
                Category = "Other"
            });

            list.Add(new Payment
            {
                Date = DateTime.UtcNow.ConvertToInt(),
                Name = "Test2",
                Amount = 420,
                Category = "Other"
            });

            list.Add(new Payment
            {
                Date = DateTime.UtcNow.ConvertToInt(),
                Name = "Test3",
                Amount = 100,
                Category = "Salary"
            });

            return list;
        }

        [HttpGet("{type}")]
        public ActionResult<List<Payment>> Get(string type) =>
            _fileManager.ReadPayments(type);
    }
}