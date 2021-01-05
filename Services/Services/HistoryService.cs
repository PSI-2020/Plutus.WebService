using Plutus.WebService.IRepos;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Plutus.WebService
{
    public class HistoryService : IHistoryService
    {
        private readonly IPaymentService _paymentService;
        public HistoryService(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public List<HistoryElement> LoadDataGrid(int index)
        {
            switch (index)
            {
                case 0:
                {
                        var list = _paymentService.GetPayments(DataType.Expense).Select(x => new HistoryElement { Date = x.Date.ConvertToDate(), Name = x.Name, Amount = x.Amount, Category = x.Category, Type = "Exp." }).ToList();
                        var incomeList = _paymentService.GetPayments(DataType.Income).Select(x => new HistoryElement { Date = x.Date.ConvertToDate(), Name = x.Name, Amount = x.Amount, Category = x.Category, Type = "Inc." }).ToList();

                        list.AddRange(incomeList);

                    return !list.Any() ? null : list.OrderByDescending(x => x.Date).ToList();
                }
                case 1:
                {
                        var list = _paymentService.GetPayments(DataType.Expense).Select(x => new HistoryElement { Date = x.Date.ConvertToDate(), Name = x.Name, Amount = x.Amount, Category = x.Category, Type = "Exp." }).OrderByDescending(x => x.Date).ToList();

                        return !list.Any() ? null : list;
                }
                case 2:
                {
                        var list = _paymentService.GetPayments(DataType.Income).Select(x => new HistoryElement { Date = x.Date.ConvertToDate(), Name = x.Name, Amount = x.Amount, Category = x.Category, Type = "Inc." }).OrderByDescending(x => x.Date).ToList();

                        return !list.Any() ? null : list;
                }
                default: return null;
            }
        }
    }
}
