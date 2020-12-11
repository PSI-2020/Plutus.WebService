using System.IO;
using Plutus.WebService.IRepos;

namespace Plutus.WebService
{
    public class LoggerRepository : ILoggerRepository
    {
        public void Log(string message) => File.AppendAllText("Storage/Log.txt", message + "\r\n");
    }
}
