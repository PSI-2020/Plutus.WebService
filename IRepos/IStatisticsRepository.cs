namespace Plutus.WebService.IRepos
{
    public interface IStatisticsRepository
    {
        public string GenerateExpenseStatistics();
        public string GenerateIncomeStatistics();
    }
}
