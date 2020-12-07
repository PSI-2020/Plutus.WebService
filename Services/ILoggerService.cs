using System.IO;

namespace Plutus.WebService
{
    interface ILogger
    {
        void Log(string message);
    }
    public class ILoggerService : ILogger
    {
        public void Log(string message)
        {
            File.AppendAllText("Storage/Log.txt", message + "\r\n");
        }
    }
}
