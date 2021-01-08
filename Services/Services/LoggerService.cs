using System.IO;
using Plutus.WebService.IRepos;

namespace Plutus.WebService
{
    public class LoggerService : ILoggerService
    {
        public LoggerService()
        {
            
        }
        public void Log(string message) { File.AppendAllText("Storage/Log.txt", message + "\r\n"); }

        private void OutputDataDeletion(object o, string name) => Log(name + " was deleted");
        
    }
}
