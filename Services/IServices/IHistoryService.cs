using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface IHistoryService
    {
        public List<HistoryElement> LoadDataGrid(int index, int page, int perPage, Filters filter);
        public int GivePageCount(int index, int perPage, Filters filter);
    }
}
