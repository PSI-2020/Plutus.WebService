﻿namespace Db.Entities
{
    public class ScheduledPayment
    {
        public int Date { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public string ScheduledPaymentId { get; set; }
        public string Frequency { get; set; }
        public bool Active { get; set; }
    }
}
