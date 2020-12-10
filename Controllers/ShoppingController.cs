using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;
using System.Collections.Generic;

namespace Plutus.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController
    {
        private readonly IShoppingBackendRepository _shoppingRepo;
        public ShoppingController(IShoppingBackendRepository shoppingRepo)
        {
            _shoppingRepo = shoppingRepo;
        }
        [HttpPost]
        public void ChargeShopping(List<ShoppingExpense> shoppingExpenses) => _shoppingRepo.ChargeShopping(shoppingExpenses);
    }
}
