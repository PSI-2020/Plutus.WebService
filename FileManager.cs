using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Plutus.WebService
{
    public enum DataType
    {
        [Description("Storage/income.xml")]
        Income,
        [Description("Storage/expenses.xml")]
        Expense,
        [Description("Storage/monthlyIncome.xml")]
        MonthlyIncome,
        [Description("Storage/monthylExpenses.xml")]
        MonthlyExpenses,
        [Description("Storage/goals.xml")]
        Goals,
        [Description("Storage/budgets.xml")]
        Budgets,
        [Description("Storage/carts.xml")]
        Carts
    }

    public class FileManager
    {
        public List<T> ReadFromFile<T>(DataType type) where T : class
        {
            var serializer = new XmlSerializer(typeof(List<T>));

            try
            {
                using (var stream = File.OpenRead(type.ToDescriptionString()))
                {
                    return serializer.Deserialize(stream) as List<T>;
                }
            }
            catch
            {
                return new List<T>();
            }
        }

        public void EditPayment(Payment payment, Payment newPayment, DataType type)
        {
            var serializer = new XmlSerializer(typeof(List<Payment>));
            var list = ReadFromFile<Payment>(type);
            list[list.IndexOf(payment)] = newPayment;

            File.WriteAllText(type.ToDescriptionString(), "");
            using (var stream = File.OpenWrite(type.ToDescriptionString()))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void AddPayment(Payment payment, DataType type)
        {
            var serializer = new XmlSerializer(typeof(List<Payment>));
            var list = ReadFromFile<Payment>(type);
            CheckDuplicates(list, payment);

            list.Add(payment);
            using (var stream = File.OpenWrite(type.ToDescriptionString()))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void CheckDuplicates(List<Payment> list, Payment payment)
        {
            var duplicates = list.Where(x => payment.Equals(x));
            if(duplicates.Any()) Debug.Print("Found " + duplicates.Count() + " duplicate payments.");
        }


        public void AddGoal(Goal goal)
        {
            var serializer = new XmlSerializer(typeof(List<Goal>));
            var list = ReadFromFile<Goal>(DataType.Goals);
            list.Add(goal);

            using (var stream = File.OpenWrite(DataType.Goals.ToDescriptionString()))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void UpdateGoals(List<Goal> list)
        {
            var serializer = new XmlSerializer(typeof(List<Goal>));
            File.WriteAllText(DataType.Goals.ToDescriptionString(), "");
            using (var stream = File.OpenWrite(DataType.Goals.ToDescriptionString()))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void AddBudget(Budget budget)
        {
            var serializer = new XmlSerializer(typeof(List<Budget>));
            var list = ReadFromFile<Budget>(DataType.Budgets);
            list.Add(budget);

            using (var stream = File.OpenWrite(DataType.Budgets.ToDescriptionString()))
            {
                serializer.Serialize(stream, list);
            }

        }

        public void UpdateBudgets(List<Budget> list)
        {
            var serializer = new XmlSerializer(typeof(List<Budget>));
            File.WriteAllText(DataType.Budgets.ToDescriptionString(), "");
            using (var stream = File.OpenWrite(DataType.Budgets.ToDescriptionString()))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void AddScheduledPayment(ScheduledPayment payment, DataType type)
        {
            var serializer = new XmlSerializer(typeof(List<ScheduledPayment>));
            var list = ReadFromFile<ScheduledPayment>(type);

            list.Add(payment);
            using (var stream = File.OpenWrite(type.ToDescriptionString()))
            {
                serializer.Serialize(stream, list);
            }

        }

        public void UpdateScheduledPayments(List<ScheduledPayment> list, DataType type)
        {
            var serializer = new XmlSerializer(typeof(List<ScheduledPayment>));
            File.WriteAllText(type.ToDescriptionString(), "");
            using (var stream = File.OpenWrite(type.ToDescriptionString()))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void SaveCarts(XElement carts) => carts.Save(DataType.Carts.ToDescriptionString());

        public XElement LoadCarts()
        {
            if (!File.Exists(DataType.Carts.ToDescriptionString())) return null;
            try
            {
                var carts = XElement.Load(DataType.Carts.ToDescriptionString());
                return carts;
            }
            catch
            {
                return new XElement("Corrupted", "true");
            }
        }
    }
}
