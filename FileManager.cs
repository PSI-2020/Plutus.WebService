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
        public List<Payment> ReadPayments(DataType type)
        {
            var serializer = new XmlSerializer(typeof(List<Payment>));

            //if (type.ToLower() == "all")
            //{
            //    var list = ReadPayments("Expense");
            //    list.AddRange(ReadPayments("Income"));
            //    return list;
            //}

            try
            {
                using (var stream = File.OpenRead(type.ToDescriptionString()))
                {
                    return serializer.Deserialize(stream) as List<Payment>;
                }
            }
            catch
            {
                return new List<Payment>();
            }
        }

        public void EditPayment(Payment payment, Payment newPayment, DataType type)
        {
            var serializer = new XmlSerializer(typeof(List<Payment>));
            var list = ReadPayments(type);
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
            var list = ReadPayments(type);
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

        public List<Goal> ReadGoals()
        {
            var serializer = new XmlSerializer(typeof(List<Goal>));

            try
            {
                using (var stream = File.OpenRead(DataType.Goals.ToDescriptionString()))
                {
                    return serializer.Deserialize(stream) as List<Goal>;
                }
            }
            catch
            {
                return new List<Goal>();
            }

        }

        public void AddGoal(string name, string amount, DateTime dueDate)
        {
            var serializer = new XmlSerializer(typeof(List<Goal>));
            var list = ReadGoals();

            var newAmount = double.Parse(amount);
            var goal = new Goal(name, newAmount, dueDate);

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
            var list = LoadBudget();

            list.Add(budget);
            using (var stream = File.OpenWrite(DataType.Budgets.ToDescriptionString()))
            {
                serializer.Serialize(stream, list);
            }

        }

        public List<Budget> LoadBudget()
        {
            var serializer = new XmlSerializer(typeof(List<Budget>));

            try
            {
                using (var stream = File.OpenRead(DataType.Budgets.ToDescriptionString()))
                {
                    return serializer.Deserialize(stream) as List<Budget>;
                }
            }
            catch
            {
                return new List<Budget>();
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
            var list = LoadScheduledPayments(type);

            list.Add(payment);
            using (var stream = File.OpenWrite(type.ToDescriptionString()))
            {
                serializer.Serialize(stream, list);
            }

        }

        public List<ScheduledPayment> LoadScheduledPayments(DataType type)
        {
            var serializer = new XmlSerializer(typeof(List<ScheduledPayment>));

            try
            {
                using (var stream = File.OpenRead(type.ToDescriptionString()))
                {
                    return serializer.Deserialize(stream) as List<ScheduledPayment>;
                }
            }
            catch
            {
                return new List<ScheduledPayment>();
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
