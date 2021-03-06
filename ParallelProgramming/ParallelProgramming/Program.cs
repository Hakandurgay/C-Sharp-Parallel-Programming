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
                Console.WriteLine("5 sanyie süren task");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                } 
                Console.WriteLine("5 saniyelik task bitti");

            },token);
            t.Start();

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

              //      Task.WaitAll(t, t2); //threadlerin ikisi de bitene kadar bekler.
             //   Task.WaitAny(t2); //thread bitene kadar bekler sonra devam eder
            //    Task.WaitAny(t2,t); //ilk hangisi bitiyorsa onu bekler
            Task.WaitAll(new[] {t, t2}, 4000, token); //4 saniye sonra devam etttir

            Console.WriteLine($"task t status is {t.Status}" );
            Console.WriteLine($"task t2 status is {t2.Status}" );

            Console.WriteLine("Main program done");
            Console.ReadKey();
            cts.Cancel();

          Console.ReadKey();
        }
    }
}
