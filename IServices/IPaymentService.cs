namespace Plutus.WebService.IRepos
{
    public interface IPaymentService
    {
        public void AddPayment(CurrentInfoHolder chi);
        public void AddCartPayment(string name, double amount, string category);

    }
}
