using LogF1.Logger;
using System;
using System.Linq;
using Xunit;

namespace UnitTestProject
{
    public class UnitTest3
    {

        [Fact]
        public void TestService()
        {
            LoggerManager.Path = "c:/temp/logS.log";

            ILogger<TestLogger> logger= LoggerManager.GetInstance<TestLogger>();
            LoggerManager.ChangeOutPut((mesg) => {
                Console.WriteLine(mesg);
            });
            for (int i = 0; i < 100; i++)
            {
                logger.Warning("test" + i);
                logger.Info("testInfo" + i);
                logger.Error("test" + i);
                logger.Fatal("test" + i);

            }
            ServiceTest serviceTest = new ServiceTest();
            serviceTest.TestMethodeError();
            serviceTest.TestMethodeError();
            serviceTest.TestMethodeOkInfo();
            Assert.Equal(2, LoggerManager.GetInstance<ServiceTest>().Messages(TypeMessage.Error).Count);
            Assert.Equal(1, LoggerManager.GetInstance<ServiceTest>().Messages(TypeMessage.Info).Count);

            ServiceTest serviceTest2 = new ServiceTest();

            serviceTest2.TestMethodeError();
            serviceTest2.TestMethodeError();
            serviceTest2.TestMethodeOkInfo();
            Assert.Equal(4, LoggerManager.GetInstance<ServiceTest>().Messages(TypeMessage.Error).Count);
            Assert.Equal(2, LoggerManager.GetInstance<ServiceTest>().Messages(TypeMessage.Info).Count);

            Assert.Equal(100, LoggerManager.GetInstance<TestLogger>().Messages(TypeMessage.Info).Count);



        }
    }
}
