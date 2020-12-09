using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Plutus.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private CartBackendService _cartService = new CartBackendService();

        [HttpGet]
        public List<string> LoadCarts() => _cartService.GiveCartNames();
        
        [HttpGet("Payments/{index}")]
        public List<CartExpense> CallCarts(int index) => _cartService.GiveExpenses(index);

        [HttpPost("{index}/{name}")]
        public void SaveCarts(int index, string name, List<CartExpense> cart) => _cartService.SaveCarts(index, name, cart);
       
        [HttpPost("Charge/{index}")]
        public void ChargeCart(int index) => _cartService.ChargeCart(index);

        [HttpDelete("{index}")]
        public void DeleteCart(int index) => _cartService.DeleteCart(index);

    }
}