using System;

namespace PlutusDb.Entities
{
    public class HistoryElement
    {
        public int HistoryElementId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public virtual Client Client { get; set; }
    }
}
