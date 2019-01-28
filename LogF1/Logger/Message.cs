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

        public Type Type { get; set; }

        public TypeMessage TypeMessage { get; set; }

        public string messageData { get; set; }

       

    }
}
