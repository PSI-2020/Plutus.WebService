using System;
using System.Runtime.Serialization;

namespace Plutus
{
    public class Payment : ISerializable, IEquatable<Payment>
    {
        public int Date { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }

        public Payment() { }

        public Payment(int date, string name, double amount, string category)
        {
            Date = date;
            Name = name;
            Amount = amount;
            Category = category;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Date", Date);
            info.AddValue("Name", Name);
            info.AddValue("Amount", Amount);
            info.AddValue("Category", Category);
        }

        public bool Equals(Payment other) => (Name, Amount, Category) == (other.Name, other.Amount, other.Category);

        public Payment(SerializationInfo info, StreamingContext context)
        {
            Date = (int)info.GetValue("Date", typeof(int));
            Name = (string)info.GetValue("Name", typeof(string));
            Amount = (double)info.GetValue("Amount", typeof(double));
            Category = (string)info.GetValue("Category", typeof(string));
        }
    }
}
