using System.Collections.Generic;

namespace Plutus.WebService.IRepos
{
    public interface IHistoryRepository
    {
        public List<All> LoadDataGrid(int index);
    }
}
