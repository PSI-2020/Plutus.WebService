using Microsoft.AspNetCore.Mvc;

namespace Plutus.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly CartService _cartService = new CartService();

        [HttpGet("LoadMessage")]
        public string GiveLoadMessage()
        {
            return _cartService.GiveLoadMessage();
        }

        [HttpGet("CartCount")]
        public int GiveCartCount()
        {
            return _cartService.GiveCartCount();
        }

        [HttpGet("{index}/CartName")]
        public string GiveNameAt(int index)
        {
            return _cartService.GiveCartNameAt(index);
        }

        [HttpGet("CurrentCartExpenseCount")]
        public int GiveExpenseCount()
        {
            return _cartService.GiveCurrentCartElemCount();
        }

        [HttpGet("CartExpenseAt/{index}")]
        public CartExpense GiveExpenseAt(int index)
        {
            return _cartService.GiveCurrentElemAt(index);
        }

        [HttpPost("{index}/SelectCart")]
        public void CartSelected(int index)
        {
            _cartService.CurrentCartSet(index);
        }

        [HttpPost("NewCart")]
        public void NewCart()
        {
            _cartService.NewCart();
        }

        [HttpPost("AddExpense")]
        public void AddExpense(CurrentInfoHolder _cih)
        {
            _cartService.AddExpenseToCart(_cih);
        }

        [HttpPost("SaveCart")]
        public void SaveNewCart(string name, int prevCart)
        {
            _cartService.SetCurrentName(name);
            _cartService.SaveCartChanges(prevCart);
        }

        [HttpDelete("DeleteCurrentCart")]
        public void DeleteCart()
        {
            _cartService.DeleteCurrent();
        }

        [HttpPost("ChargeCurrentCart")]
        public void ChargeCart()
        {
            _cartService.ChargeCart();
        }

        [HttpDelete("{index}/DeleteCart")]
        public void RemoveExpense(int index)
        {
            _cartService.RemoveExpenseCurrentAt(index);
        }

        [HttpPost("ChargeShopping")]
        public void ChargeShopping(ShoppingService service)
        {
            service.ChargeShopping();
        }

    }
}
