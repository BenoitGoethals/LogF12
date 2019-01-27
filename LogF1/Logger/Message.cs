using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogF1.Logger
{
   public class Message
    {
        public Message()
        {

        }

        public Type type { get; set; }

        public TypeMessage typeMessage { get; set; }

        public string message { get; set; }

       

    }
}
