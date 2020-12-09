namespace Plutus.WebService.IRepos
{
    public interface IStatisticsRepository
    {
        public string GenerateExpenseStatistics(IFileManagerRepository manager);
        public string GenerateIncomeStatistics(IFileManagerRepository manager);
    }
}
