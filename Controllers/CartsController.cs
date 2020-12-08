using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Plutus.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private CartService _cartService = new CartService();

        [HttpGet]
        public List<string> LoadCarts() => _cartService.GiveCarts();
        [HttpGet("Payments/{index}")]
        public List<CartExpense> CallCarts(int index) => _cartService.GiveExpenses(index);


        [HttpPost]
        public void SaveCarts(List<Cart> carts) => _cartService.SaveCarts(carts);

    }
}