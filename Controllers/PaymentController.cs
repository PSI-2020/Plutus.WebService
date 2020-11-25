using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Plutus;
using System.Linq;
using System.Threading.Tasks;

namespace Plutus.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        readonly FileManager fileManager = new FileManager();

        //https://localhost:44334/Payment?type=Expense
        [HttpGet]
        public IEnumerable<Payment> Get(string type)
        {
            return fileManager.ReadPayments(type);
        }
    }
}
