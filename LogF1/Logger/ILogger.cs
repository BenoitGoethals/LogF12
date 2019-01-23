using System.Collections.Generic;

namespace LogF1.Logger
{
    public interface ILogger<T> where T : class 
    {

        void Warning(string description);
        void Info(string description);
        void Error(string description);

        void Fatal(string description);


        List<string> Messages(TypeMessage message);
    }
}