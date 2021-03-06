using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ParallelProgramming
{
    class Program
    {
     
        static void Main(string[] args)
        {

            var cts =new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                Console.WriteLine("press any key to disarm: you have 5 seconds");
                bool cancelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled? "bomb disarmed." : "boom");
            },token);
            t.Start();
            Console.ReadKey();
            cts.Cancel();

          Console.ReadKey();
        }
    }
}
