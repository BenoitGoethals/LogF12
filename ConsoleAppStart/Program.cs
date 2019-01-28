using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppStart
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("test");
            ServiceOne one=new ServiceOne();
            one.TestMethodeOkInfo();
            one = null;
            Console.WriteLine("end");
            Console.ReadLine();

        }
    }
}
