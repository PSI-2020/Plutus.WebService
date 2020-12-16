using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;
using System.Collections.Generic;

namespace Plutus.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController
    {
        private readonly IShoppingBackendService _shoppingService;
        public ShoppingController(IShoppingBackendService shoppingService) => _shoppingService = shoppingService;
        [HttpPost]
        public void ChargeShopping(List<ShoppingExpense> shoppingExpenses) => _shoppingService.ChargeShopping(shoppingExpenses);
    }
}
