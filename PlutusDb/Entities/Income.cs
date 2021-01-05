namespace PlutusDb.Entities
{
    public class Income
    {
        public int Date { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public int IncomeId { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
