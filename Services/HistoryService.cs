using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Plutus.WebService
{
    public class HistoryService
    {
        private readonly FileManager _fileManager = new FileManager();

        public List<All> LoadDataGrid(int index)
        {
            switch (index)
            {
                case 0:
                {
                        var list = _fileManager.ReadFromFile<Payment>(DataType.Expense).Select(x => new All { Date = x.Date.ConvertToDate(), Name = x.Name, Amount = x.Amount, Category = x.Category, Type = "Exp." }).ToList();
                        var incomeList = _fileManager.ReadFromFile<Payment>(DataType.Income).Select(x => new All { Date = x.Date.ConvertToDate(), Name = x.Name, Amount = x.Amount, Category = x.Category, Type = "Inc." }).ToList();

                        list.AddRange(incomeList);

                    return !list.Any() ? null : list.OrderByDescending(x => x.Date).ToList();
                }
                case 1:
                {
                        var list = _fileManager.ReadFromFile<Payment>(DataType.Expense).Select(x => new All { Date = x.Date.ConvertToDate(), Name = x.Name, Amount = x.Amount, Category = x.Category, Type = "Exp." }).OrderByDescending(x => x.Date).ToList();

                        return !list.Any() ? null : list;
                }
                case 2:
                {
                        var list = _fileManager.ReadFromFile<Payment>(DataType.Income).Select(x => new All { Date = x.Date.ConvertToDate(), Name = x.Name, Amount = x.Amount, Category = x.Category, Type = "Inc." }).OrderByDescending(x => x.Date).ToList();

                        return !list.Any() ? null : list;
                }
                default: return null;
            }
        }
    }
}
