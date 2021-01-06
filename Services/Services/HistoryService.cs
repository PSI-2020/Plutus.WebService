using Plutus.WebService.IRepos;
using System;
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

        public List<HistoryElement> LoadDataGrid(int index, int page, int perPage, Filters filter)
        {
            var list = new List<HistoryElement>();

            switch (index)
            {
                case 0:
                {
                    list = AddingToList(list, "Exp.");
                    list = AddingToList(list, "Inc.");
                    list = (filter.Used) ? FilteringList(list, filter) : list;
                    list = PagingList(list, page, perPage);
                    return list;
                }
                case 1:
                {
                    list = AddingToList(list, "Exp.");
                    list = (filter.Used) ? FilteringList(list, filter) : list;
                    list = PagingList(list, page, perPage);
                    return list;
                }
                case 2:
                {
                    list = AddingToList(list, "Inc.");
                    list = (filter.Used) ? FilteringList(list, filter) : list;
                    list = PagingList(list, page, perPage);
                    return list;
                }
                default: return null;
            }
        }
        public int GivePageCount(int index, int perPage, Filters filter)
        {
            perPage = (perPage == 0) ? 1 : perPage;
            var list = LoadDataGrid(index, 0, int.MaxValue, filter);
            var count = list.Count / perPage;
            return count++;
        }
        private List<HistoryElement> AddingToList(List<HistoryElement> prevlist, string type)
        {
            var dt = (type == "Inc.") ? DataType.Income : DataType.Expense;
            var list = _paymentService.GetPayments(dt).Select(x => new HistoryElement { Date = x.Date.ConvertToDate(), Name = x.Name, Amount = x.Amount, Category = x.Category, Type = type }).ToList();
            prevlist.AddRange(list);
            prevlist.OrderByDescending(x => x.Date.ConvertToInt()).ToList();
            return prevlist;
        }
        private List<HistoryElement> PagingList(List<HistoryElement> prevlist, int page, int perPage)
        {
            page--;
            var listSkip = prevlist.Skip(page * perPage).ToList();
            var listTake = listSkip.Take(perPage).ToList();
            return listTake;
        }
        private List<HistoryElement> FilteringList(List<HistoryElement> prevlist, Filters filter)
        {
            if (filter.NameFiter) prevlist = FilterName(prevlist, filter.NameFiterString);
            var lexp = new List<HistoryElement>();
            var linc = new List<HistoryElement>();
            if (filter.ExpFlag != 0) lexp = FilterExp(prevlist, filter.ExpFlag);
            if (filter.IncFlag != 0) linc = FilterInc(prevlist, filter.IncFlag);
            var finall = new List<HistoryElement>();
            finall.AddRange(lexp);
            finall.AddRange(linc);
            prevlist = finall;
            if (filter.AmountFilter != 0) prevlist = FilterAmount(prevlist, filter.AmountFilter, filter.AmountFrom, filter.AmountTo);
            if (filter.DateFilter) prevlist = FilterDate(prevlist, filter.DateFrom, filter.DateTo);
            return prevlist;
        }

        private List<HistoryElement> FilterName(List<HistoryElement> prevlist, string name) => prevlist.Where(x => x.Name.ToLower() == name.ToLower()).ToList();

        private List<HistoryElement> FilterAmount(List<HistoryElement> prevlist, int flag, double from, double to)
        {
            var t = (flag == 1) ? to : double.MaxValue;
            var f = (flag == 2) ? from : -1;
            if (flag == 3)
            {
                t = to;
                f = from;
            }
            return prevlist.Where(x => x.Amount <= t).Where(x => x.Amount >= f).ToList();
        }

        private List<HistoryElement> FilterDate(List<HistoryElement> prevlist, int from, int to)
        {
            to += 86399;
            return prevlist.Where(x => x.Date.ConvertToInt() <= from)
                       .Where(x => x.Date.ConvertToInt() >= to)
                       .ToList();
        }

        private List<HistoryElement> FilterInc(List<HistoryElement> prevlist, int incFlag)
        {
            var list = new List<HistoryElement>();
            var selectedICategories = new List<string>();
            if (incFlag >= 16)
            {
                selectedICategories.Add("Rent");
                incFlag -= 16;
            }
            if (incFlag >= 8)
            {
                selectedICategories.Add("Sale");
                incFlag -= 8;
            }
            if (incFlag >= 4)
            {
                selectedICategories.Add("Sale");
                incFlag -= 4;
            }
            if (incFlag >= 2)
            {
                selectedICategories.Add("Gift");
                incFlag -= 2;
            }
            if (incFlag >= 1)
            {
                selectedICategories.Add("Salary");
            }
            foreach (var payment in prevlist)
            {
                foreach(var category in selectedICategories)
                {
                    if((payment.Category == category) && (payment.Type == "Inc."))
                    {
                        list.Add(payment);
                    }
                }
            }
            return list;
        }

        private List<HistoryElement> FilterExp(List<HistoryElement> prevlist, int expFlag)
        {
            var list = new List<HistoryElement>();
            var selectedICategories = new List<string>();
            if (expFlag >= 256)
            {
                selectedICategories.Add("Transport");
                expFlag -= 256;
            }
            if (expFlag >= 128)
            {
                selectedICategories.Add("Other");
                expFlag -= 128;
            }
            if (expFlag >= 64)
            {
                selectedICategories.Add("Entertainment");
                expFlag -= 64;
            }
            if (expFlag >= 32)
            {
                selectedICategories.Add("School");
                expFlag -= 32;
            }
            if (expFlag >= 16)
            {
                selectedICategories.Add("Health");
                expFlag -= 16;
            }
            if (expFlag >= 8)
            {
                selectedICategories.Add("Clothes");
                expFlag -= 8;
            }
            if (expFlag >= 4)
            {
                selectedICategories.Add("Restaurant");
                expFlag -= 4;
            }
            if (expFlag >= 2)
            {
                selectedICategories.Add("Bills");
                expFlag -= 2;
            }
            if (expFlag >= 1)
            {
                selectedICategories.Add("Groceries");
            }
            foreach (var payment in prevlist)
            {
                foreach (var category in selectedICategories)
                {
                    if ((payment.Category == category) && (payment.Type == "Exp."))
                    {
                        list.Add(payment);
                    }
                }
            }
            return list;
        }


    }
}
