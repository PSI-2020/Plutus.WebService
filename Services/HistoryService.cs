using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Plutus.WebService
{
    public class HistoryService
    {
        private readonly FileManager _fileManager = new FileManager();

        public object LoadDataGrid(int index)
        {
            switch (index)
            {
                case 0:
                { 
                    var list = _fileManager.ReadPayments(DataType.Expense).Select(x => new { Date = x.Date.ConvertToDate(), x.Name, x.Amount, x.Category, Type = "Exp." }).ToList();
                    var incomeList = _fileManager.ReadPayments(DataType.Income).Select(x => new { Date = x.Date.ConvertToDate(), x.Name, x.Amount, x.Category, Type = "Inc." }).ToList();

                    list.AddRange(incomeList);

                    return !list.Any() ? null : (object)list.OrderByDescending(x => x.Date).ToList();
                }
                case 1:
                {
                    var list = _fileManager.ReadPayments(DataType.Expense).Select(x => new { DATE = x.Date.ConvertToDate(), NAME = x.Name, AMOUNT = x.Amount, CATEGORY = x.Category })
                    .OrderByDescending(x => x.DATE).ToList();

                    return !list.Any() ? null : (object)list;
                }
                case 2:
                {
                    var list = _fileManager.ReadPayments(DataType.Income).Select(x => new { DATE = x.Date.ConvertToDate(), NAME = x.Name, AMOUNT = x.Amount, CATEGORY = x.Category })
                    .OrderByDescending(x => x.DATE).ToList();

                    return !list.Any() ? null : (object)list;
                }
                default: return null;
            }
        }
    }
}
