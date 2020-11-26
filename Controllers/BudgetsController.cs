using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plutus.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        readonly FileManager _fileManager = new FileManager();
        private List<Budget> ReadBudgets()
        {
            var list = _fileManager.LoadBudget();
            return list;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Budget> Get()
        {
            return ReadBudgets();
        }

        // GET api/<ValuesController>/5
        /*[HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
