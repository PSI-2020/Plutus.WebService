using Microsoft.AspNetCore.Mvc;
using Plutus.WebService.IRepos;
using System.Collections.Generic;

namespace Plutus.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {

        private readonly ICartBackendRepository _cartRepository;
        public CartsController(ICartBackendRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public List<string> LoadCarts() => _cartRepository.GiveCartNames();

        [HttpGet("Payments/{index}")]
        public List<CartExpense> CallCarts(int index) => _cartRepository.GiveExpenses(index);

        [HttpPost("{index}/{name}")]
        public void SaveCarts(int index, string name, List<CartExpense> cart) => _cartRepository.SaveCarts(index, name, cart);
       
        [HttpPost("Charge/{index}")]
        public void ChargeCart(int index) => _cartRepository.ChargeCart(index);

        [HttpDelete("{index}")]
        public void DeleteCart(int index) => _cartRepository.DeleteCart(index);
    }
}