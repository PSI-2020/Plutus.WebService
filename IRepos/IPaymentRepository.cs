namespace Plutus.WebService.IRepos
{
    public interface IPaymentRepository
    {
        public void AddPayment(CurrentInfoHolder chi);
        public void AddCartPayment(string name, double amount, string category);

    }
}
