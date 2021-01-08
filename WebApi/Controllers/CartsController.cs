using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;
using System.Collections.Generic;

namespace Plutus.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {

        private readonly ICartBackendService _cartService;
        public CartsController(ICartBackendService cartService) => _cartService = cartService;

        [HttpGet]
        public List<Services.Models.CartInfo> LoadCarts() => _cartService.GiveCartNames();

        [HttpGet("Payments/{index}")]
        public List<CartExpense> CallCarts(int id) => _cartService.GiveExpenses(id);

        [HttpPost("{id}/{name}")]
        public void SaveCarts(int id, string name, [FromBody] List<CartExpense> cart) => _cartService.ChangeCart(id, name, cart);
        [HttpPost("{name}")]
        public void PostNew(string name) => _cartService.NewCart(name);

        [HttpPost("Charge/{index}")]
        public void ChargeCart(int index) => _cartService.ChargeCart(index);

        [HttpDelete("{index}")]
        public void DeleteCart(int index) => _cartService.DeleteCart(index);
    }
}