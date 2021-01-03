using PlutusDb.Entities;
using System;

namespace Db.Entities
{
    public class Goal
    {
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public int GoalId { get; set; }
        public virtual Client Client { get; set; }
    }
}
