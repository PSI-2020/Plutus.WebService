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

        [HttpGet("Payments/{id}")]
        public List<CartExpense> CallCarts(int id) => _cartService.GiveExpenses(id);

        [HttpPost("{id}/{name}")]
        public void SaveCarts(int id, string name) => _cartService.RenameCart(id, name);
        [HttpPost("{name}")]
        public void PostNew(string name) => _cartService.NewCart(name);
        [HttpPost("{id}/edit")]
        public void UpdateExpense(int id, [FromBody] CartExpense exp) => _cartService.EditExpense(id, exp);
        [HttpPost("{id}/add")]
        public void AddExpense(int id, [FromBody] CartExpense exp) => _cartService.AddExpense(id, exp);
        [HttpDelete("{id}/{expid}")]
        public void DeleteExpense(int id, int expid) => _cartService.RemoveExpense(id, expid);

        [HttpPost("Charge/{id}")]
        public void ChargeCart(int index) => _cartService.ChargeCart(index);

        [HttpDelete("{id}")]
        public void DeleteCart(int id) => _cartService.DeleteCart(id);
    }
}