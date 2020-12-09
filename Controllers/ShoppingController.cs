using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Plutus.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController
    {
        private readonly ShoppingBackendService _shoppingService = new ShoppingBackendService();
        [HttpPost]
        public void ChargeShopping(List<ShoppingExpense> shoppingExpenses) => _shoppingService.ChargeShopping(shoppingExpenses);
        [HttpGet]
        public string ReceiveShoppingResult() => _shoppingService.GiveShoppingResult();
    }
}
