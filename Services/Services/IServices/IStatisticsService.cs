namespace Plutus.WebService.IRepos
{
    public interface IStatisticsService
    {
        public string GenerateExpenseStatistics();
        public string GenerateIncomeStatistics();
    }
}
