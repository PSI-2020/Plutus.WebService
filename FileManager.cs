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
        private static readonly string _directoryPath = Directory.GetCurrentDirectory();
        private readonly string _income = _directoryPath + "/db/income.xml";
        private readonly string _expenses = _directoryPath + "/db/expenses.xml";
        private readonly string _monthlyIncome = _directoryPath + "/db/monthlyIncome.xml";
        private readonly string _monthlyExpenses = _directoryPath + "/db/monthylExpenses.xml";
        public readonly string fontPathMaconodo = _directoryPath + "/True GUI/GUI resources/Macondo.ttf";
        public readonly string fontPathLilita = _directoryPath + "/True GUI/GUI resources/LilitaOne.ttf";

        public string GetFilePath(string type)
        {
            return type switch
            {
                "Income" => _income,
                "Expense" => _expenses,
                "MonthlyIncome" => _monthlyIncome,
                "MonthlyExpenses" => _monthlyExpenses,
                _ => null,
            };
        }

        public List<Payment> ReadPayments(string type)
        {
            var serializer = new XmlSerializer(typeof(List<Payment>));

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
            if (duplicates.Any()) Debug.Print("Found " + duplicates.Count() + " duplicate payments.");
        }
    }
}
