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

        private List<Payment> payments()
        {
            var list = fileManager.ReadPayments("Expense");
            list.AddRange(fileManager.ReadPayments("Income"));
            return list;
        }

        [HttpGet]
        public IEnumerable<Payment> Get()
        {
            return payments();
        }
    }
}
