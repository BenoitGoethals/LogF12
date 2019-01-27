using LogF1.Logger.ObserableCollection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LogF1.Logger
{


   public enum LOGGER_OutputType {
        XMl,
        TXT


    }



    public static class LoggerManager
    {
        private readonly static InternalObserver internalObserver = new InternalObserver(WriteXMLAsync);

        static LoggerManager()
        {
            observableLoggerPool.Subscribe(internalObserver);
        }

        public static  void ChangeOutPut (Action<Message> action)
        {
             internalObserver.LogWriterTypeAsync = action;
        }

        public static void ChangeOutPutType(LOGGER_OutputType outputType )
        {
            switch (outputType)
            {
                case LOGGER_OutputType.TXT:
                    internalObserver.LogWriterTypeAsync = WriteTextAsync;
                    break;

                case LOGGER_OutputType.XMl:
                    internalObserver.LogWriterTypeAsync = WriteXMLAsync;
                    break;
            }
        }

        private static  void WriteTextAsync(Message text)
        {

            byte[] encodedText = Encoding.Unicode.GetBytes(text.message);

            using (FileStream sourceStream = new FileStream(Path,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                 sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }


        private static  void WriteXMLAsync(Message text)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Async = true;
            if (!File.Exists(Path))
            {
                XNamespace empNM = "urn:lst-log:log";

                XDocument xDoc = new XDocument(
                            new XDeclaration("1.0", "UTF-16", null),
                            new XElement(empNM + "LOGS",
                                new XElement("log",
                                    new XElement("time",DateTime.Now.ToLongDateString()),
                                    new XElement(nameof(text.type), text.type.ToString()),
                                    new XElement(nameof(text.typeMessage), text.typeMessage.ToString()),
                                    new XElement(nameof(text.message), text.message)
                                    )));

                StringWriter sw = new StringWriter();
                XmlWriter xWrite = XmlWriter.Create(sw,settings);
                 xWrite.FlushAsync();
                xDoc.Save(xWrite);
               
                xWrite.Close();
                xDoc.Save(Path);
                
               
            }
            else
            {
              
                StringWriter sw = new StringWriter();
                XmlWriter xWrite = XmlWriter.Create(sw, settings);
                // Save to Disk

                XElement xEle = XElement.Load(Path);
                xEle.Add(new XElement("log",
                                      new XElement("time", DateTime.Now.ToLongDateString()),
                                    new XElement(nameof(text.type), text.type.ToString()),
                                    new XElement(nameof(text.typeMessage), text.typeMessage.ToString()),
                                    new XElement(nameof(text.message), text.message)
                                    ));

                 xWrite.FlushAsync();
                xEle.Save(xWrite);
            
                xWrite.Close();
                xEle.Save(Path);



            }
        }


        public static string Path { get; set; }

        public static IObserver<Message> Observer ;
        
        private static Dictionary<string, object> loggers = new Dictionary<string, object>();

        private static ObservableLoggerPool observableLoggerPool = new ObservableLoggerPool();


        public static ILogger<T> GetInstance<T>() where T : class
        {
                  
            if (loggers.ContainsKey(typeof(T).ToString()))
                return (ILogger<T>)loggers[key: typeof(T).ToString()];
            ILogger<T> log = new Logger<T>(AddMessage, Messages);
            loggers.Add(typeof(T).ToString(), (log));
            return log;
        }


        private static void AddMessage(Message message)
        {
            observableLoggerPool.AddMessage(message);
          
        }
        

        private static List<string> Messages(Type type, TypeMessage typeMessage)
        {
            return observableLoggerPool.Messages(type, typeMessage);
        }
        

       private  class InternalObserver : IObserver<Message>, IDisposable
        {
            



            public InternalObserver(Action<Message> logWriterTypeAsync)
            {
                this.LogWriterTypeAsync = logWriterTypeAsync;
            }

            public Action<Message> LogWriterTypeAsync { get; set; }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public void OnCompleted()
            {
                throw new NotImplementedException();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(Message value)
            {
                LogWriterTypeAsync?.Invoke(value);
            }
        


           

          
        }


    }




}