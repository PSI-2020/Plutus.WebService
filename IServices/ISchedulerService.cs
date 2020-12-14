namespace Plutus.WebService.IRepos
{
    public interface ISchedulerService
    {
        public void CheckPayments();
        public string ShowPayment(int index, DataType type);
        public void ChangeStatus(int index, DataType type, bool status);
        public void DeletePayment(int index, DataType type);
    }
}
