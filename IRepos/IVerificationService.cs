namespace Plutus.WebService.IRepos
{
    public interface IVerificationService
    {
        public string VerifyData(string name, string amount, string category);
    }
}
