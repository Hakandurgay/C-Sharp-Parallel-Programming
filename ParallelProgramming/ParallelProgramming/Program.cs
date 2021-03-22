using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    
        static CountdownEvent cte=new CountdownEvent(5);
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew((() =>
                {
                    Console.WriteLine($"entering task {Task.CurrentId}");
                    Thread.Sleep(2000);
                    cte.Signal(); //Barrierdeki signal and wait metodu yerine burada ayrı ayrı olarak var. her signal metodu çalıştığında 5'ten bir düşer. 
                    Console.WriteLine($"exiting task {Task.CurrentId}");
                }));
            }

            var finalTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"waiting for other tasks to complete in {Task.CurrentId}");
                cte.Wait();  //5, sıfır olana kadar bekler. 
                Console.WriteLine("all tasks completed");
            });
            finalTask.Wait();
        }
    }
}
