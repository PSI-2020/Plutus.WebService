//using System;
//using Microsoft.AspNetCore.Mvc;

//namespace Plutus.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ValuesController : ControllerBase
//    {
//        [HttpGet]
//        public ActionResult<string> Get()
//        {
//            string text = "";
//            //Program.MainForm.Invoke(new Action(() =>
//            //{
//            //    text = Program.MainForm.expenseTextLabel.Text;
//            //}));
//            return text;
//        }

//        [HttpGet("{id}")]
//        public ActionResult Get(string id)
//        {
//            Program.MainForm.Invoke(new Action(() =>
//            {
//                Program.MainForm.expenseTextLabel.Text = id;
//            }));
//            return Ok();
//        }
//    }
//}