using System;
using System.Runtime.Serialization;

namespace Plutus.WebService
{
    public class All : ISerializable
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }

        public All() { }

        public All(DateTime date, string name, double amount, string category, string type)
        {
            Date = date;
            Name = name;
            Amount = amount;
            Category = category;
            Type = type;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Date", Date);
            info.AddValue("Name", Name);
            info.AddValue("Amount", Amount);
            info.AddValue("Category", Category);
            info.AddValue("Type", Type);
        }

        public All(SerializationInfo info, StreamingContext context)
        {
            Date = (DateTime)info.GetValue("Date", typeof(int));
            Name = (string)info.GetValue("Name", typeof(string));
            Amount = (double)info.GetValue("Amount", typeof(double));
            Category = (string)info.GetValue("Category", typeof(string));
            Type = (string)info.GetValue("Type", typeof(string));
        }
    }
}
