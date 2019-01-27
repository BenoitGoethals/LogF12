using Xunit;
using LogF1.Logger.ObserableCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject;
using System.Threading;

namespace LogF1.Logger.ObserableCollection.Tests
{
    public class ObservableLoggerPoolTests
    {
        [Fact()]
        public void SubscribeTest()
        {
            Obs obs = new Obs();
            ObservableLoggerPool observableLoggerPool = new ObservableLoggerPool();
            
            observableLoggerPool.AddMessage(new Message() {

                message = "test",
                type = typeof(ServiceTest),
                typeMessage = TypeMessage.Error

            });
            Assert.Equal(1, observableLoggerPool.Messages(typeof(ServiceTest), TypeMessage.Error).Count);
            
            
        }

        
    }
}