using LogF1.Logger.ObserableCollection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace LogF1.Logger
{
    public static class LoggerManager
    {

        public static string Path { get; set; } = "c:/temp/log.xml";

    

        private static readonly ObservableDictionary<string, object> Loggers = new ObservableDictionary<string, object>();

        private static readonly ObservableLoggerPool ObservableLoggerPool = new ObservableLoggerPool();

       

        private static readonly InternalObserver ObserverInternal = new InternalObserver(WriteXmlAsync);

        static LoggerManager()
        {
            ObservableLoggerPool.Subscribe(ObserverInternal);
            Loggers.ToObservable().Subscribe(onNext: ((s) =>
            {
                Console.WriteLine(s);
            }));
        }

        public static  void ChangeOutPut (Action<Message> action)
        {
             ObserverInternal.LogWriterTypeAsync = action;
        }

        public static void ChangeOutPutType(LoggerOutputType outputType )
        {
            switch (outputType)
            {
                case LoggerOutputType.Txt:
                    ObserverInternal.LogWriterTypeAsync = WriteTextAsync;
                    break;

                case LoggerOutputType.XMl:
                    ObserverInternal.LogWriterTypeAsync = WriteXmlAsync;
                    break;
            }
        }

        private static  void WriteTextAsync(Message text)
        {

            byte[] encodedText = Encoding.Unicode.GetBytes(text.messageData);

            using (FileStream sourceStream = new FileStream(Path,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                 sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }


        private static  void WriteXmlAsync(Message text)
        {
            XmlWriterSettings settings = new XmlWriterSettings {Async = true};
            if (!File.Exists(Path))
            {
                XNamespace empNm = "urn:lst-log:log";

                XDocument xDoc = new XDocument(
                            new XDeclaration("1.0", "UTF-16", null),
                            new XElement(empNm + "LOGS",
                                new XElement("log",
                                    new XElement("time",DateTime.Now.ToLongDateString()),
                                    new XElement(nameof(text.Type), text.Type.ToString()),
                                    new XElement(nameof(text.TypeMessage), text.TypeMessage.ToString()),
                                    new XElement(nameof(text.messageData), text.messageData)
                                    )));
             
                using (StringWriter sw = new StringWriter())
                {
                  
                XmlWriter xWrite = XmlWriter.Create(sw,settings);
                 xWrite.FlushAsync();
                xDoc.Save(xWrite);
               
                xWrite.Close();
                xDoc.Save(Path);
                }

            }
            else
            {
              
            
                // Save to Disk

                XElement xEle = XElement.Load(Path);
                xEle.Add(new XElement("log",
                                      new XElement("time", DateTime.Now.ToLongDateString()),
                                    new XElement(nameof(text.Type), text.Type.ToString()),
                                    new XElement(nameof(text.TypeMessage), text.TypeMessage.ToString()),
                                    new XElement(nameof(text.messageData), text.messageData)
                                    ));
                using (StringWriter sw = new StringWriter())
                { 
                    XmlWriter xWrite = XmlWriter.Create(sw, settings);
                xWrite.FlushAsync();
                xEle.Save(xWrite);
            
                xWrite.Close();
                xEle.Save(Path);
                }


            }
        }


        public static ILogger<T> GetInstance<T>() where T : class
        {
                  
            if (Loggers.ContainsKey(typeof(T).ToString()))
                return (ILogger<T>)Loggers[key: typeof(T).ToString()];
            ILogger<T> log = new Logger<T>(AddMessage, Messages);
            Loggers.Add(typeof(T).ToString(), (log));
            return log;
        }


        private static void AddMessage(Message message)
        {
            ObservableLoggerPool.AddMessage(message);
          
        }
        

        private static List<string> Messages(Type type, TypeMessage typeMessage)
        {
            return ObservableLoggerPool.Messages(type, typeMessage);
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