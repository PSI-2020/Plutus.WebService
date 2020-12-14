using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface IHistoryService
    {
        public List<HistoryElement> LoadDataGrid(int index);
    }
}
