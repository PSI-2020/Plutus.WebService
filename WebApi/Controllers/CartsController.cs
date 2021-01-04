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
        public List<string> LoadCarts() => _cartService.GiveCartNames();

        [HttpGet("Payments/{index}")]
        public List<CartExpense> CallCarts(int index) => _cartService.GiveExpenses(index);

        [HttpPost("{index}/{name}")]
        public void SaveCarts(int index, string name, List<CartExpense> cart) => _cartService.SaveCarts(index, name, cart);

        [HttpPost("/Xamarin/{index}/{name}")]
        public void NewCart(int index, string name) => _cartService.SaveCarts(index, name, new List<CartExpense>());

        [HttpPut("/Xamarin/{index}/{name}")]
        public void ChangeCart(int index, string name, List<CartExpense> cart) => _cartService.SaveCarts(index, name, cart);

        [HttpPost("Charge/{index}")]
        public void ChargeCart(int index) => _cartService.ChargeCart(index);

        [HttpDelete("{index}")]
        public void DeleteCart(int index) => _cartService.DeleteCart(index);
    }
}