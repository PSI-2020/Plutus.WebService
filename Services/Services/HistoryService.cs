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

        public List<HistoryElement> LoadDataGrid(int index, int page, int perPage)
        {
            var list = new List<HistoryElement>();

            switch (index)
            {
                case 0:
                {
                    list = AddingToList(list, "Exp.");
                    list = AddingToList(list, "Inc.");
                    list = PagingList(list, page, perPage);
                    return !list.Any() ? null : list;
                }
                case 1:
                {
                    list = AddingToList(list, "Exp.");
                    list = PagingList(list, page, perPage);
                    return !list.Any() ? null : list;
                }
                case 2:
                {
                    list = AddingToList(list, "Inc.");
                    list = PagingList(list, page, perPage);
                    return !list.Any() ? null : list;
                }
                default: return null;
            }
        }
        public int GivePageCount(int index, int perPage)
        {
            var list = LoadDataGrid(index, 0, int.MaxValue);
            var count = list.Count / perPage;
            return count++;
        }
        private List<HistoryElement> AddingToList(List<HistoryElement> prevlist, string type)
        {
            var dt = (type == "Inc.") ? DataType.Income : DataType.Expense;
            var list = _paymentService.GetPayments(dt).Select(x => new HistoryElement { Date = x.Date.ConvertToDate(), Name = x.Name, Amount = x.Amount, Category = x.Category, Type = type }).ToList();
            prevlist.AddRange(list);
            prevlist.OrderByDescending(x => x.Date).ToList();
            return prevlist;
        }
        private List<HistoryElement> PagingList(List<HistoryElement> prevlist, int page, int perPage)
        {
            page--;
            var listSkip = prevlist.Skip(page * perPage).ToList();
            var listTake = listSkip.Take(perPage).ToList();
            return listTake;
        }
    }
}
