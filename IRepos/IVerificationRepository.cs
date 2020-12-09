namespace Plutus.WebService.IRepos
{
    public interface IVerificationRepository
    {
        public string VerifyData(string name, string amount, string category);
    }
}
