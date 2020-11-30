﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Plutus.WebService
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly FileManager _fileManager = new FileManager();

        [HttpGet]
        public ActionResult<List<Payment>> Get()
        {
            var list = _fileManager.ReadPayments(DataType.Expense);
            list.AddRange(_fileManager.ReadPayments(DataType.Income));

            return list;
        }

        [HttpGet("{type}")]
        public ActionResult<List<Payment>> Get(string type) =>
            _fileManager.ReadPayments(type);

        //[HttpPost]
        //public ActionResult<Payment> Post(Payment payment)
        //{
        //    return payment;
        //}
    }
}