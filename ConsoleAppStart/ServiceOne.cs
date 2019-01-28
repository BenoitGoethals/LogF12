using LogF1.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppStart
{
   public class ServiceOne : IDisposable
   {
        public ServiceOne()
        {
            _logger.Info("created");
        }

        private readonly ILogger<ServiceOne> _logger = LoggerManager.GetInstance<ServiceOne>();



        public void TestMethodeOkInfo()
        {
            _logger.Info("ok");
        }


        public void Dispose()
        {
            _logger.Info("Dispose");
        }

        public void Deconstruct(out ILogger<ServiceOne> logger)
        {
            logger = _logger;
        }
    }
}
