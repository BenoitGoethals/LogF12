using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using LogF1.Logger;
using Xunit;

namespace UnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {
            LoggerManager.Path = "c:/temp/log.txt";
            ILogger<TestLogger> logger=LoggerManager.GetInstance<TestLogger>();


            for (int i = 0; i < 100; i++)
            {
                logger.Warning("test"+i);
                logger.Info("testInfo" + i);
                logger.Error("test" + i);
                logger.Fatal("test" + i);
               
            }

         

            Assert.Equal(100,logger.Messages(TypeMessage.Warning).Count);
            Assert.Equal(100, logger.Messages(TypeMessage.Error).Count);


        }
    }
}
