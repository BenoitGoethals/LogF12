using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LogF1.Logger
{
    public static class LoggerManager
    {
        public static string Path { get; set; }
        
        private static readonly Dictionary<Tuple<Type, TypeMessage>, List<String>> Dictionary = new Dictionary<Tuple<Type, TypeMessage>, List<string>>();

        public static ILogger<T>  GetInstance<T>() where T : class
        {
            return new Logger<T>(AddMessage, Messages);
        }


        private static  void AddMessage(Type type, TypeMessage typeMessage, string message)
        {
            var typeMsgLogger = new Tuple<Type, TypeMessage>(type, typeMessage);
            if (Dictionary.ContainsKey(key: typeMsgLogger))
            {
                Dictionary[typeMsgLogger]?.Add(message);
            }
            else
            {
                Dictionary.Add(typeMsgLogger, new List<string>() { message });
            }
             WriteTextAsync(Path, text: $"{type.FullName}  ---  {typeMessage} ---   {message + Environment.NewLine}").GetAwaiter();
        }



        private static List<string> Messages(Type type, TypeMessage typeMessage)
        {
            var typeMsgLogger = new Tuple<Type, TypeMessage>(type, typeMessage);
            if (Dictionary.ContainsKey(key: typeMsgLogger))
            {
                return Dictionary[typeMsgLogger];
            }
            return null;
        }




        static async Task WriteTextAsync(string filePath, string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }

    }
}