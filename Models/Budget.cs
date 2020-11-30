using System;
using System.Runtime.Serialization;

namespace Plutus.WebService
{ 
    public class Budget : ISerializable
    {
        public string Name { get; set; }
        public string Category { get; set; }
        private double _sum;
        public double Sum
        {
            get => _sum;
            set => _sum = value >= 0 ? value : throw new ArgumentOutOfRangeException("The sum of budget cannot be negative");
        }
        public int From { get; set; }
        public int To { get; set; }
        public Budget(string name, string category, double sum, DateTime from, DateTime to)
        {
            Name = name;
            Category = category;
            Sum = sum;
            From = (int)from.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            To = (int)to.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public Budget()
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("From", From);
            info.AddValue("To", To);
            info.AddValue("Name", Name);
            info.AddValue("Sum", Sum);
            info.AddValue("Category", Category);
        }

        public Budget(SerializationInfo info, StreamingContext context)
        {
            From = (int)info.GetValue("From", typeof(int));
            To = (int)info.GetValue("To", typeof(int));
            Name = (string)info.GetValue("Name", typeof(string));
            Sum = (double)info.GetValue("Sum", typeof(double));
            Category = (string)info.GetValue("Category", typeof(string));
        }
    }
}
