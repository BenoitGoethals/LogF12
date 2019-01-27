using LogF1.Logger;
using System;
using System.Linq;
using Xunit;

namespace UnitTestProject
{
    public class UnitTest2
    {
       


        [Fact]
        public void StressTestMethod1()
        {
            LoggerManager.Path = "c:/temp/log.xml";
            ILogger<ServiceTest> logger = LoggerManager.GetInstance<ServiceTest>();
            ILogger<ServiceTest> logger2 = LoggerManager.GetInstance<ServiceTest>();
            ILogger<ServiceTest> logger3 = LoggerManager.GetInstance<ServiceTest>();


            for (int i = 0; i < 10; i++)
            {
                logger.Warning("test" + i);
                logger.Info("testInfo" + i);
                logger.Error("test" + i);
                logger.Fatal("test" + i);

                logger2.Warning("test" + i);
                logger2.Info("testInfo" + i);
                logger2.Error("test" + i);
                logger2.Fatal("test" + i);
                logger3.Warning("test" + i);
                logger3.Info("testInfo" + i);
                logger3.Error("test" + i);
                logger3.Fatal("test" + i);

            }



            Assert.Equal(30, logger.Messages(TypeMessage.Warning).Count);
            Assert.Equal(30, logger.Messages(TypeMessage.Error).Count);


        }

    
    }
}
