using PlutusDb.Entities;

namespace Db.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int Date { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public string PaymentType { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
