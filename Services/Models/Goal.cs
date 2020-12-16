using System;
using System.Runtime.Serialization;

namespace Plutus.WebService
{
    public class Goal : ISerializable
    {
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }

        public Goal() { }
        public Goal(string name, double amount, DateTime date)
        {
            Name = name;
            Amount = amount;
            DueDate = date;
        }
        public Goal(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Amount = (double)info.GetValue("Amount", typeof(double));
            DueDate = (DateTime)info.GetValue("Due Date", typeof(DateTime));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Amount", Amount);
            info.AddValue("Due Date", DueDate);
        }

    }
}
