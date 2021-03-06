﻿using System;
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


        private readonly Action<Message> _addMessage;
        private readonly Func<Type, TypeMessage, List<string>> _messages;


        public Logger(Action<Message> addMessage, Func<Type, TypeMessage, List<string>> messages)
        {
            this._addMessage = addMessage;
            this._messages = messages;
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
            return _messages?.Invoke(typeof (T), typeMessage);
        }


        private void AddNewMessage(TypeMessage typeM, string description)
        {
            Message message = new Message()
            {
                messageData = description,
                TypeMessage = typeM,
                Type = (typeof(T))



            };
            _addMessage?.Invoke(message);

        }
    }
}
