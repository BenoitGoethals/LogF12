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
            _dictionaryMessages.CollectionChanged += DictionaryMessages_CollectionChanged;
        }

        private void DictionaryMessages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // TBD
        }

        private readonly List<IObserver<Message>> _observers = new List<IObserver<Message>>();


        private ObservableDictionary<Tuple<Type, TypeMessage>, List<string>> _dictionaryMessages = new ObservableDictionary<Tuple<Type, TypeMessage>, List<string>>();


        public void AddMessage(Message message)
        {

            var typeMsgLogger = new Tuple<Type, TypeMessage>(message.Type, message.TypeMessage);
            if (_dictionaryMessages.ContainsKey(key: typeMsgLogger))
            {
                _dictionaryMessages[typeMsgLogger]?.Add(message.messageData);
            }
            else
            {
                _dictionaryMessages.Add(typeMsgLogger, new List<string>() { message.messageData });
            }


            _observers.ForEach(t => t.OnNext(message));

        }

        public List<string> Messages(Type type, TypeMessage typeMessage)
        {
            var typeMsgLogger = new Tuple<Type, TypeMessage>(type, typeMessage);
            if (_dictionaryMessages.ContainsKey(key: typeMsgLogger))
            {
                return new List<string>(_dictionaryMessages[typeMsgLogger].ToArray());
            }
            return null;
        }

        public IDisposable Subscribe(IObserver<Message> observer)
        {
            _observers.Add(observer);
            return Disposable.Empty; ;
        }

        public void Dispose() => _dictionaryMessages = null;
    }
}
