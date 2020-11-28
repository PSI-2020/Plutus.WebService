using System;

namespace Plutus
{
   public static class DaysLeftExtension
    {
        public static string CalculateDaysLeft(this Goal goal) => goal.DueDate < DateTime.Now ? 0.ToString("F0") : (goal.DueDate - DateTime.Now).TotalDays.ToString("F0");
    }
}
