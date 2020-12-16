using System;
using System.Runtime.Serialization;

namespace Plutus.WebService
{
    public class ScheduledPayment : ISerializable
    {
        public int Date { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public string Id { get; set; }
        public string Frequency { get; set; }
        public bool Active { get; set; }

        public ScheduledPayment() { }

        public ScheduledPayment(DateTime date, string name, double amount, string category, string id, string frequency, bool status)
        {
            Date = date.ConvertToInt();
            Name = name;
            Amount = amount;
            Category = category;
            Id = id;
            Frequency = frequency;
            Active = status;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Date", Date);
            info.AddValue("Name", Name);
            info.AddValue("Amount", Amount);
            info.AddValue("Category", Category);
            info.AddValue("Id", Id);
            info.AddValue("Frequency", Frequency);
            info.AddValue("Status", Active);
        }

        public ScheduledPayment(SerializationInfo info, StreamingContext context)
        {
            Date = (int)info.GetValue("Date", typeof(int));
            Name = (string)info.GetValue("Name", typeof(string));
            Amount = (double)info.GetValue("Amount", typeof(double));
            Category = (string)info.GetValue("Category", typeof(string));
            Id = (string)info.GetValue("Id", typeof(string));
            Frequency = (string)info.GetValue("Frequency", typeof(string));
            Active = (bool)info.GetValue("Status", typeof(bool));
        }
    }
}
