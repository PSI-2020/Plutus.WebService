using PlutusDb.Entities;
using System.Collections.Generic;

namespace Db.Entities
{
    public class Cart
    {
        public int CartId { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public virtual List<CartExpense> CartExpenses { get; set; }
    }
}
