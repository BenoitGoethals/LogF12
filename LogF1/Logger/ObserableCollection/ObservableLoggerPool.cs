using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogF1.Logger.ObserableCollection
{
    public sealed class ObservableLoggerPool : IObservable<Message>, IDisposable
    {
        public ObservableLoggerPool()
        {
            DictionaryMessages.CollectionChanged += DictionaryMessages_CollectionChanged;
        }

        private void DictionaryMessages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // TBD
        }

        private List<IObserver<Message>> observers = new List<IObserver<Message>>();


        private ObservableDictionary<Tuple<Type, TypeMessage>, List<string>> DictionaryMessages = new ObservableDictionary<Tuple<Type, TypeMessage>, List<string>>();


        public void AddMessage(Message message)
        {

            var typeMsgLogger = new Tuple<Type, TypeMessage>(message.type, message.typeMessage);
            if (DictionaryMessages.ContainsKey(key: typeMsgLogger))
            {
                DictionaryMessages[typeMsgLogger]?.Add(message.message);
            }
            else
            {
                DictionaryMessages.Add(typeMsgLogger, new List<string>() { message.message });
            }


            observers.ForEach(t => t.OnNext(message));

        }

        public List<string> Messages(Type type, TypeMessage typeMessage)
        {
            var typeMsgLogger = new Tuple<Type, TypeMessage>(type, typeMessage);
            if (DictionaryMessages.ContainsKey(key: typeMsgLogger))
            {
                return new List<string>(DictionaryMessages[typeMsgLogger].ToArray());
            }
            return null;
        }

        public IDisposable Subscribe(IObserver<Message> observer)
        {
            observers.Add(observer);
            return Disposable.Empty; ;
        }

        public void Dispose()
        {
            DictionaryMessages = null;
        }
    }
}
