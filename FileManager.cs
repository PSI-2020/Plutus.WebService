using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Plutus
{


    public class FileManager
    {
        private static readonly string _directoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        private readonly string _income = _directoryPath + "/WebService/Storage/income.xml";
        private readonly string _expenses = _directoryPath + "/WebService/Storage/expenses.xml";
        private readonly string _monthlyIncome = _directoryPath + "/WebService/Storage/monthlyIncome.xml";
        private readonly string _monthlyExpenses = _directoryPath + "/WebService/Storage/monthylExpenses.xml";
        private readonly string _goals = _directoryPath + "/WebService/Storage/goals.xml";
        private readonly string _budgets = _directoryPath + "/WebService/Storage/budgets.xml";
        private readonly string _carts = _directoryPath + "/WebService/Storage/carts.xml";
        public readonly string fontPathMaconodo = _directoryPath + "/True GUI/GUI resources/Macondo.ttf";
        public readonly string fontPathLilita = _directoryPath + "/True GUI/GUI resources/LilitaOne.ttf";

        public string GetFilePath(string type)
        {
            return type.ToLower() switch
            {
                "income" => _income,
                "expense" => _expenses,
                "monthlyincome" => _monthlyIncome,
                "monthlyexpenses" => _monthlyExpenses,
                _ => null,
            };
        }

        public List<Payment> ReadPayments(string type)
        {
            var serializer = new XmlSerializer(typeof(List<Payment>));
            if (type.ToLower() == "all")
            {
                var list = ReadPayments("Expense");
                list.AddRange(ReadPayments("Income"));
                return list;
            }

            try
            {
                using (var stream = File.OpenRead(GetFilePath(type)))
                {
                    return serializer.Deserialize(stream) as List<Payment>;
                }
            }
            catch
            {
                return new List<Payment>();
            }
        }
        public void EditPayment(Payment payment, Payment newPayment, string type)
        {
            var serializer = new XmlSerializer(typeof(List<Payment>));
            var list = ReadPayments(type);
            list[list.IndexOf(payment)] = newPayment;
            type = GetFilePath(type);

            File.WriteAllText(type, "");
            using (var stream = File.OpenWrite(type))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void AddPayment(Payment payment, string type)
        {
            var serializer = new XmlSerializer(typeof(List<Payment>));
            var list = ReadPayments(type);
            CheckDuplicates(list, payment);
            type = GetFilePath(type);

            list.Add(payment);
            using (var stream = File.OpenWrite(type))
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
                using (var stream = File.OpenRead(_goals))
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
            using (var stream = File.OpenWrite(_goals))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void UpdateGoals(List<Goal> list)
        {
            var serializer = new XmlSerializer(typeof(List<Goal>));
            File.WriteAllText(_goals, "");
            using (var stream = File.OpenWrite(_goals))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void AddBudget(Budget budget)
        {
            var serializer = new XmlSerializer(typeof(List<Budget>));
            var list = LoadBudget();

            list.Add(budget);
            using (var stream = File.OpenWrite(_budgets))
            {
                serializer.Serialize(stream, list);
            }

        }

        public List<Budget> LoadBudget()
        {
            var serializer = new XmlSerializer(typeof(List<Budget>));

            try
            {
                using (var stream = File.OpenRead(_budgets))
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
            File.WriteAllText(_budgets, "");
            using (var stream = File.OpenWrite(_budgets))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void AddScheduledPayment(ScheduledPayment payment, string type)
        {
            var serializer = new XmlSerializer(typeof(List<ScheduledPayment>));
            var list = LoadScheduledPayments(type);

            list.Add(payment);
            using (var stream = File.OpenWrite(GetFilePath(type)))
            {
                serializer.Serialize(stream, list);
            }

        }

        public List<ScheduledPayment> LoadScheduledPayments(string type)
        {
            var serializer = new XmlSerializer(typeof(List<ScheduledPayment>));

            try
            {
                using (var stream = File.OpenRead(GetFilePath(type)))
                {
                    return serializer.Deserialize(stream) as List<ScheduledPayment>;
                }
            }
            catch
            {
                return new List<ScheduledPayment>();
            }
        }

        public void UpdateScheduledPayments(List<ScheduledPayment> list, string type)
        {
            var serializer = new XmlSerializer(typeof(List<ScheduledPayment>));
            File.WriteAllText(GetFilePath(type), "");
            using (var stream = File.OpenWrite(GetFilePath(type)))
            {
                serializer.Serialize(stream, list);
            }
        }

        public void SaveCarts(XElement carts) => carts.Save(_carts);

        public XElement LoadCarts()
        {
            if (!File.Exists(_carts)) return null;
            try
            {
                var carts = XElement.Load(_carts);
                return carts;
            }
            catch
            {
                return new XElement("Corrupted", "true");
            }
        }
    }
}
