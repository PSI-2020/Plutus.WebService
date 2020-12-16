using Plutus.WebService.IRepos;

namespace Plutus.WebService
{
    public class VerificationService : IVerificationService
    {
        public string VerifyData(string name = "undefined", string amount = "0", string category = "undefined")
        {
            return string.IsNullOrEmpty(name)
                ? "Name cannot be empty!"
                : !double.TryParse(amount, out _)
                ? "Amount is not a number!"
                : string.IsNullOrEmpty(category) ? "Please choose a category!" : "";

        }
    }
}