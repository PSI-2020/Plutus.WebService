using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Plutus.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private FileManager _fm = new FileManager();

        [HttpGet]
        public XElement LoadCarts() => _fm.LoadCarts();

        [HttpPost]
        public void SaveCarts(XElement carts) => _fm.SaveCarts(carts);

    }
}