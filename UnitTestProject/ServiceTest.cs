using LogF1.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
   public class ServiceTest
    {

        private ILogger<ServiceTest> logger = LoggerManager.GetInstance<ServiceTest>();



        public void TestMethodeOkInfo() {
            logger.Info("ok");
        }



        public void TestMethodeError() {

            try
            {
                logger.Error("Div error");
                var a = 0 / 5;
                Console.WriteLine(a);
            }
            catch (Exception)
            {
                logger.Error("Div error");
              
            }

        }




    }
}
