namespace Plutus.WebService.IRepos
{
    public interface IStatisticsService
    {
        public string GenerateExpenseStatistics();
        public string GenerateIncomeStatistics();
        public decimal CategorySum(string category, DataType type);
        public decimal AllCategoriesSum(DataType type);
    }
}
