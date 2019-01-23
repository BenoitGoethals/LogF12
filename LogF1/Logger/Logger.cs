using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LogF1.Logger
{
    public class Logger<T>:ILogger<T> where T : class
    {


        private Action<Type, TypeMessage, string> addMessage;
        private Func<Type, TypeMessage, List<string>> messages;


        public Logger(Action<Type, TypeMessage, string> addMessage, Func<Type, TypeMessage, List<string>> messages)
        {
            this.addMessage = addMessage;
            this.messages = messages;
        }


        public void Warning(string description)
        {
           AddNewMessage(TypeMessage.Warning, description);
        }

        public void Info(string description)
        {
            AddNewMessage(TypeMessage.Info, description);
        }

        public void Error(string description)
        {
            AddNewMessage(TypeMessage.Error, description);
        }

        public void Fatal(string description)
        {
            AddNewMessage(TypeMessage.Fatal, description);
        }

        public List<string> Messages(TypeMessage typeMessage)
        {
            return messages?.Invoke(typeof (T), typeMessage);
        }


        private void AddNewMessage(TypeMessage typeMessage, string description)
        {
            addMessage?.Invoke(typeof(T), typeMessage, description);

        }
    }
}
