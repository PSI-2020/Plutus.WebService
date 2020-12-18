﻿namespace Db.Entities
{
    public class CartExpense
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public bool Active { get; set; }
        public int CartExpenseId { get; set; }
    }
}